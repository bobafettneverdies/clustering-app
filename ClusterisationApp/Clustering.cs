using System.Data.SqlClient;

namespace ClusterisationApp
{
    class Clustering
    {
        public void StartClusteringAlg(float r)
        {
            Profit pf = new Profit();
            
            //создание первого пустого кластера
            SqlConnection con = new SqlConnection(DBCon.Con);
            con.Open();
            var cmd = new SqlCommand("INSERT INTO [Cluster] ([N], [W],[S]) VALUES (0, 0, 0)", con);
            cmd.ExecuteNonQuery();
            con.Close();
            

            //Фаза 1 - инициализация
            con.Open();
            cmd = new SqlCommand("SELECT [Doc_ID] FROM [Doc] WHERE Cluster_ID IS NULL", con);
            SqlDataReader DocReader = cmd.ExecuteReader();
            while (DocReader.Read()) 
            {
                long maxprofitdeltaID = 0; //ID кластера дающего максимальный прирост
                float maxprofitdelta = 0; //размер максимального прироста критерия Profit
                long Wmax=0, Smax=0; //параметры кластера после добавления в него документа
                
                DocTags dt = new DocTags((long)DocReader[0]);
                
                //поиск кластера дающего максимальный прирост Profit
                SqlConnection clustercon = new SqlConnection(DBCon.Con);
                clustercon.Open();
                var clcmd = new SqlCommand("SELECT [Cluster_ID] FROM [Cluster]", clustercon);
                SqlDataReader ClReader = clcmd.ExecuteReader();
                while (ClReader.Read())
                {
                    Cluster cl1 = new Cluster((long)ClReader[0]);
                    float deltacl = pf.DeltaAdd(cl1, dt, r);
                    if (deltacl > maxprofitdelta) { maxprofitdelta = deltacl; maxprofitdeltaID = (long)ClReader[0]; }
                }
                clustercon.Close();

                pf.profitmodify(maxprofitdelta);
                Cluster clnew = new Cluster(maxprofitdeltaID);

                //запись данных в БД
                clustercon.Open();
                clcmd = new SqlCommand("UPDATE [Doc] SET [Cluster_ID]=@cid WHERE [Doc_ID]=@did", clustercon);
                clcmd.Parameters.AddWithValue("@cid", maxprofitdeltaID);
                clcmd.Parameters.AddWithValue("@did", (long)DocReader[0]);
                clcmd.ExecuteNonQuery();
                clustercon.Close();

                for (long i = 0; i < dt.GetTagIDs().Length; i++)
                {
                    SqlConnection ticcon = new SqlConnection(DBCon.Con);
                    ticcon.Open();

                    var ticcmd = new SqlCommand("SELECT [TagInCl_ID] FROM [TaginCluster] WHERE [Tag_ID]=@tid AND [Cluster_ID]=@cid", ticcon);
                    ticcmd.Parameters.AddWithValue("@tid", dt.GetTagIDs()[i]);
                    ticcmd.Parameters.AddWithValue("@cid", maxprofitdeltaID);
                    SqlDataReader ticReader = ticcmd.ExecuteReader();
                    if (ticReader.Read())
                    {
                        SqlConnection ticcon1 = new SqlConnection(DBCon.Con);
                        ticcon1.Open();
                        var ticcmd1 = new SqlCommand("UPDATE [TaginCluster] SET [Occ]=Occ+1 WHERE [TagInCl_ID]=@id", ticcon1);
                        ticcmd1.Parameters.AddWithValue("@id", (long)ticReader[0]);
                        ticcmd1.ExecuteNonQuery();
                        ticcon1.Close(); 
                    }
                    else
                    {
                        clustercon.Open();
                        clcmd = new SqlCommand("INSERT INTO [TagInCluster] ([Tag_ID], [Cluster_ID], [Occ]) VALUES (@tid, @cid, 1)", clustercon);
                        clcmd.Parameters.AddWithValue("@tid", dt.GetTagIDs()[i]);
                        clcmd.Parameters.AddWithValue("@cid", maxprofitdeltaID);
                        clcmd.ExecuteNonQuery();
                        clustercon.Close();
                    }
                    ticcon.Close();
                }

                //пересчет характеристик кластера
                clnew.ConvertDBFields();

                clustercon.Open();
                clcmd = new SqlCommand("SElECT [Cluster_ID] FROM [Cluster] WHERE W=0", clustercon);
                ClReader = clcmd.ExecuteReader();
                if (ClReader.Read()) clustercon.Close(); //если пустых кластеров не осталось, добавляем новый
                else
                {
                    SqlConnection con1 = new SqlConnection(DBCon.Con);
                    con1.Open();
                    var cmd1 = new SqlCommand("INSERT INTO [Cluster] ([N], [W],[S]) VALUES (0, 0, 0)", con1);
                    cmd1.ExecuteNonQuery();
                    con1.Close();
                    clustercon.Close();
                }
            }
            con.Close();
            
            //Фаза 2 - итерация
            
            bool moved = false; //маркер перемещения документов между кластерами

            do //пока документы перемещаются между кластерами
            {
                bool itermoved = false; //маркер перемещения документов между кластерами на текущей итерации
                con.Open();
                cmd = new SqlCommand("SELECT [Doc_ID], [Cluster_ID] FROM [Doc]", con);
                DocReader = cmd.ExecuteReader();
                while (DocReader.Read())
                {
                    bool localmoved = false;
                    
                    long maxprofitdeltaID = 0; //ID кластера дающего максимальный прирост
                    float maxprofitdelta = 0; //размер максимального прироста критерия Profit
                    long Wbasic = 0, Sbasic = 0; //параметры кластера из которого удаляется документ
                    long Wnew = 0, Snew = 0; //параметры кластера в который добавляется документ
                    DocTags dt = new DocTags((long)DocReader[0]); //список идентификаторов тегов текущего документа
                    Cluster clbasic = new Cluster((long)DocReader[1]); //кластер к которому изначально принадлежал документ
                    
                    SqlConnection clustercon = new SqlConnection(DBCon.Con);
                    clustercon.Open();
                    var clcmd = new SqlCommand("SELECT [Cluster_ID] FROM [Cluster]", clustercon);
                    SqlDataReader ClReader = clcmd.ExecuteReader(); 
                    while (ClReader.Read())
                    {
                        Cluster cl1 = new Cluster((long)ClReader[0]);
                        float deltacl = pf.DeltaAdd(cl1, dt, r) + pf.DeltaSub(clbasic, dt, r);
                        if (deltacl > maxprofitdelta) { maxprofitdelta = deltacl; maxprofitdeltaID = (long)ClReader[0]; localmoved = true; }
                    }
                    
                    clustercon.Close();

                    if (!localmoved) continue; //если документ не переместился
                    else
                    {
                        itermoved = true;
                        pf.profitmodify(maxprofitdelta);
                        Cluster clnew = new Cluster(maxprofitdeltaID);

                        clustercon.Open();
                        clcmd = new SqlCommand("UPDATE [Doc] SET [Cluster_ID]=@cid WHERE [Doc_ID]=@did", clustercon);
                        clcmd.Parameters.AddWithValue("@cid", maxprofitdeltaID);
                        clcmd.Parameters.AddWithValue("@did", (long)DocReader[0]);
                        clcmd.ExecuteNonQuery();
                        clustercon.Close();

                        for (long i = 0; i < dt.GetTagIDs().Length; i++)
                        {
                            SqlConnection ticcon = new SqlConnection(DBCon.Con);
                            ticcon.Open();
                            var ticcmd = new SqlCommand("UPDATE [TaginCluster] SET [Occ]=Occ-1 WHERE [Tag_ID]=@tid AND [Cluster_ID]=@cid", ticcon);
                            ticcmd.Parameters.AddWithValue("@tid", dt.GetTagIDs()[i]);
                            ticcmd.Parameters.AddWithValue("@cid", clbasic.getID());
                            ticcmd.ExecuteNonQuery();
                            ticcon.Close();

                            ticcon.Open();
                            ticcmd = new SqlCommand("SELECT [TagInCl_ID] FROM [TaginCluster] WHERE [Tag_ID]=@tid AND [Cluster_ID]=@cid", ticcon);
                            ticcmd.Parameters.AddWithValue("@tid", dt.GetTagIDs()[i]);
                            ticcmd.Parameters.AddWithValue("@cid", maxprofitdeltaID);
                            SqlDataReader ticReader = ticcmd.ExecuteReader();
                            if (ticReader.Read())
                            {
                                SqlConnection ticcon1 = new SqlConnection(DBCon.Con);
                                ticcon1.Open();
                                var ticcmd1 = new SqlCommand("UPDATE [TaginCluster] SET [Occ]=Occ+1 WHERE [TagInCl_ID]=@id", ticcon1);
                                ticcmd1.Parameters.AddWithValue("@id", (long)ticReader[0]);
                                ticcmd1.ExecuteNonQuery();
                                ticcon1.Close();
                            }
                            else
                            {
                                clustercon.Open();
                                clcmd = new SqlCommand("INSERT INTO [TagInCluster] ([Tag_ID], [Cluster_ID], [Occ]) VALUES (@tid, @cid, 1)", clustercon);
                                clcmd.Parameters.AddWithValue("@tid", dt.GetTagIDs()[i]);
                                clcmd.Parameters.AddWithValue("@cid", maxprofitdeltaID);
                                clcmd.ExecuteNonQuery();
                                clustercon.Close();
                            }
                            ticcon.Close();
                        }

                        clbasic.DeleteAllEmptyTagInDocs(); //очистка таблиц от записей о тегах, встречающихся в кластере 0 раз

                        //пересчет характеристик кластера из которого удалили документ
                        clbasic.ConvertDBFields();
                        //пересчет характеристик кластера в который добавили документ                        
                        clnew.ConvertDBFields();
                        
                        clustercon.Open();
                        clcmd = new SqlCommand("SElECT [Cluster_ID] FROM [Cluster] WHERE W=0", clustercon);
                        ClReader = clcmd.ExecuteReader();
                        if (ClReader.Read()) clustercon.Close(); // если не осталось пустых кластеров доба
                        else
                        {
                            SqlConnection con1 = new SqlConnection(DBCon.Con);
                            con1.Open();
                            var cmd1 = new SqlCommand("INSERT INTO [Cluster] ([N], [W],[S]) VALUES (0, 0, 0)", con1);
                            cmd1.ExecuteNonQuery();
                            con1.Close();
                            clustercon.Close();
                        }
                    }
                }
                con.Close();

                moved = itermoved;
            } while (!moved);

            con.Open();//удаляем все пустые кластеры
            cmd = new SqlCommand("delete from Cluster where NOT EXISTS(SElect * from Doc Where Doc.Cluster_ID=Cluster.Cluster_ID)", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}

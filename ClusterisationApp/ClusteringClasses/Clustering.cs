using System.Data.SqlClient;

namespace ClusterisationApp.ClusteringClasses
{
    public class Clustering
    {
        public void StartClusteringAlg(float r, string connectionstring)
        {
            Profit pf = new Profit();

            //создание первого пустого кластера
            DBClusterMethods.CreateEmptyCluster(connectionstring);

            //Фаза 1 - инициализация
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            var cmd = new SqlCommand("SELECT [Doc_ID] FROM [Doc] WHERE Cluster_ID IS NULL", con);
            SqlDataReader DocReader = cmd.ExecuteReader();
            while (DocReader.Read())
            {
                long maxprofitdeltaId = 0; //ID кластера дающего максимальный прирост
                float maxprofitdelta = 0; //размер максимального прироста глобального критерия Profit

                DocTags dt = new DocTags((long)DocReader[0], connectionstring);

                //поиск кластера дающего максимальный прирост Profit
                SqlConnection clustercon = new SqlConnection(connectionstring);
                clustercon.Open();
                var clcmd = new SqlCommand("SELECT [Cluster_ID] FROM [Cluster]", clustercon);
                SqlDataReader ClReader = clcmd.ExecuteReader();
                while (ClReader.Read())
                {
                    Cluster cl1 = new Cluster((long)ClReader[0], connectionstring);
                    float deltacl = pf.DeltaAdd(cl1, dt, r, connectionstring);
                    if (deltacl > maxprofitdelta) { maxprofitdelta = deltacl; maxprofitdeltaId = (long)ClReader[0]; }
                }
                clustercon.Close();

                pf.Profitmodify(maxprofitdelta);
                Cluster clnew = new Cluster(maxprofitdeltaId, connectionstring);

                DBClusterMethods.UpdateClusterIDInDoc(maxprofitdeltaId, (long)DocReader[0], connectionstring);

                for (long i = 0; i < dt.GetTagIDs().Length; i++) DBClusterMethods.UpdateTagInClusterOccPlus1(dt.GetTagIDs()[i], maxprofitdeltaId, connectionstring);
                

                //пересчет характеристик кластера
                clnew.ConvertDBFields(connectionstring);

                clustercon.Open();
                clcmd = new SqlCommand("SElECT [Cluster_ID] FROM [Cluster] WHERE W=0", clustercon);
                ClReader = clcmd.ExecuteReader();
                if (ClReader.Read()) clustercon.Close(); //если пустых кластеров не осталось, добавляем новый
                else { DBClusterMethods.CreateEmptyCluster(connectionstring); clustercon.Close(); }
            }
            con.Close();

            //Фаза 2 - итерация

            bool moved = false; //флаг перемещения документов между кластерами

            do //пока документы перемещаются между кластерами
            {
                bool itermoved = false; //маркер перемещения документов между кластерами на текущей итерации
                con.Open();
                cmd = new SqlCommand("SELECT [Doc_ID], [Cluster_ID] FROM [Doc]", con);
                DocReader = cmd.ExecuteReader();
                while (DocReader.Read())
                {
                    bool localmoved = false;

                    long maxprofitdeltaId = 0; //ID кластера дающего максимальный прирост
                    float maxprofitdelta = 0; //размер максимального прироста критерия Profit
                    DocTags dt = new DocTags((long)DocReader[0], connectionstring); //список идентификаторов тегов текущего документа
                    Cluster clbasic = new Cluster((long)DocReader[1], connectionstring); //кластер к которому изначально принадлежал документ

                    SqlConnection clustercon = new SqlConnection(connectionstring);
                    clustercon.Open();
                    var clcmd = new SqlCommand("SELECT [Cluster_ID] FROM [Cluster]", clustercon);
                    SqlDataReader ClReader = clcmd.ExecuteReader();
                    while (ClReader.Read())
                    {
                        Cluster cl1 = new Cluster((long)ClReader[0], connectionstring);
                        float deltacl = pf.DeltaAdd(cl1, dt, r, connectionstring) + pf.DeltaSub(clbasic, dt, r, connectionstring);
                        if (deltacl > maxprofitdelta) { maxprofitdelta = deltacl; maxprofitdeltaId = (long)ClReader[0]; localmoved = true; }
                    }

                    clustercon.Close();

                    if (localmoved) 
                    {
                        itermoved = true;
                        pf.Profitmodify(maxprofitdelta);
                        Cluster clnew = new Cluster(maxprofitdeltaId, connectionstring);

                        DBClusterMethods.UpdateClusterIDInDoc(maxprofitdeltaId, (long)DocReader[0], connectionstring);

                        for (long i = 0; i < dt.GetTagIDs().Length; i++)
                        {
                            SqlConnection ticcon = new SqlConnection(connectionstring);
                            ticcon.Open();
                            var ticcmd = new SqlCommand("UPDATE [TaginCluster] SET [Occ]=Occ-1 WHERE [Tag_ID]=@tid AND [Cluster_ID]=@cid", ticcon);
                            ticcmd.Parameters.AddWithValue("@tid", dt.GetTagIDs()[i]);
                            ticcmd.Parameters.AddWithValue("@cid", clbasic.getID());
                            ticcmd.ExecuteNonQuery();
                            ticcon.Close();

                            DBClusterMethods.UpdateTagInClusterOccPlus1(dt.GetTagIDs()[i], maxprofitdeltaId, connectionstring);
                        }

                        clbasic.DeleteAllEmptyTagInCluster(connectionstring); //очистка таблиц от записей о тегах, встречающихся в кластере 0 раз

                        //пересчет характеристик кластера из которого удалили документ
                        clbasic.ConvertDBFields(connectionstring);
                        //пересчет характеристик кластера в который добавили документ                        
                        clnew.ConvertDBFields(connectionstring);

                        clustercon.Open();
                        clcmd = new SqlCommand("SElECT [Cluster_ID] FROM [Cluster] WHERE W=0", clustercon);
                        ClReader = clcmd.ExecuteReader();
                        if (ClReader.Read()) clustercon.Close();
                        else { DBClusterMethods.CreateEmptyCluster(connectionstring); clustercon.Close();}
                    }
                }
                con.Close();

                moved = itermoved;
            } while (!moved);

            con.Close();

            DBClusterMethods.DeleteAllEmptyClustersFromDataBase(connectionstring);
        }
    }
}

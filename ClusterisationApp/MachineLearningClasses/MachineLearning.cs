using System;
using System.Data.SqlClient;

namespace ClusterisationApp.MachineLearningClasses
{
    class MachineLearning
    {
        public void StartLearningProcess(long wordsindoccount, long mintagcount, long mintagindoccount, string connectionstring)
        {

            //извлечение id документов из базы данных для цикла

            SqlConnection selectDocConnection = new SqlConnection(connectionstring);
            selectDocConnection.Open();
            var cmd = new SqlCommand("SELECT [Doc_ID] FROM [Doc]", selectDocConnection);
            SqlDataReader selectDocReadereader = cmd.ExecuteReader();
            //цикл по всем документам базы данных
            while (selectDocReadereader.Read())
            {
                long i = (long)selectDocReadereader[0];
                Document doc = new Document(i, connectionstring);
                doc.Normalise();

                string[] docwords = doc.getbody().Split(' ', '/', '\\');
                string[] tags = new string[wordsindoccount];
                int[] tagcount = new int[wordsindoccount];
                int minnum = 0;

                //выбор wordisdoccount слов в документе с максимальным tf
                for (int j = 0; j < docwords.Length; j++)
                {
                    int wordcount = 0;
                    if (docwords[j] == "" || docwords[j] == " ") continue;
                    for (int j1 = j; j1 < docwords.Length; ++j1)
                    {
                        if (docwords[j1] == docwords[j])
                        {
                            wordcount++;
                            if (j1 != j) docwords[j1] = "";
                        }
                    }
                    for (int j1 = 0; j1 < tagcount.Length; j1++)
                        if (tagcount[j1] < tagcount[minnum]) minnum = j1;
                    if (wordcount > tagcount[minnum]) { tagcount[minnum] = wordcount; tags[minnum] = docwords[j]; }
                }


                for (int j = 0; j < tagcount.Length; j++)
                {
                    if (tags[j] != null)
                    {
                        SqlConnection con1 = new SqlConnection(connectionstring);
                        con1.Open();


                        var cmd1 = new SqlCommand("SELECT [TagL_ID] FROM TagLearning WHERE [TagLWord]=@tag", con1);
                        cmd1.Parameters.AddWithValue("@tag", tags[j]);

                        SqlDataReader datareader1 = cmd1.ExecuteReader();
                        if (datareader1.Read())
                        {
                            long ID1 = (long)datareader1[0];
                            SqlConnection con2 = new SqlConnection(connectionstring);
                            con2.Open();
                            var cmd2 = new SqlCommand("UPDATE [TagLearning] SET [TagCount]=[Tagcount]+1 WHERE [TagL_ID]=@id", con2);
                            cmd2.Parameters.AddWithValue("@ID", ID1);
                            cmd2.ExecuteNonQuery();
                            var cmd3 = new SqlCommand("INSERT INTO [TagLearningInDoc] ([Doc_Id], [TagL_ID]) VALUES (@doc, @tag)", con2);
                            cmd3.Parameters.AddWithValue("@doc", i);
                            cmd3.Parameters.AddWithValue("@tag", ID1);
                            cmd3.ExecuteNonQuery();
                            con2.Close();
                        }
                        else
                        {
                            SqlConnection con2 = new SqlConnection(connectionstring);
                            con2.Open();
                            var cmd4 = new SqlCommand("INSERT INTO [TagLearning] ([TagLWord], [TagCount]) VALUES (@word, 1)", con2);
                            cmd4.Parameters.AddWithValue("@word", tags[j]);
                            cmd4.ExecuteNonQuery();
                            con2.Close();

                            long taglid = 0;
                            con2.Open();
                            cmd4 = new SqlCommand("SELECT TagL_ID FROM TagLearning WHERE TagLWord=@word", con2);
                            cmd4.Parameters.AddWithValue("@word", tags[j]);
                            SqlDataReader taglReader = cmd4.ExecuteReader();
                            if (taglReader.Read()) taglid = (long)taglReader[0];
                            con2.Close();

                            con2.Open();
                            var cmd3 = new SqlCommand("INSERT INTO [TagLearningInDoc] ([Doc_Id], [TagL_ID]) VALUES (@doc, @tag)", con2);
                            cmd3.Parameters.AddWithValue("@doc", i);
                            cmd3.Parameters.AddWithValue("@tag", taglid);
                            cmd3.ExecuteNonQuery();
                            con2.Close();
                        }

                        con1.Close();
                    }
                }
            }
            selectDocConnection.Close();

            //Выборка ключевых слов из полученного списка
            long maxID = 0;
            SqlConnection countcon = new SqlConnection(connectionstring);
            countcon.Open();
            var cmdc = new SqlCommand("SELECT MAX([Doc_ID]) FROM [Doc]", countcon);
            SqlDataReader datareaderc = cmdc.ExecuteReader();
            if (datareaderc.Read()) maxID = (long)(datareaderc[0]);
            countcon.Close();
            MachineLearningArray mla = new MachineLearningArray(connectionstring);
            long taggeddoccount = 0;
            selectDocConnection.Open();
            var tagselect = new SqlCommand("SELECT [TagL_ID], [TagLWord], [TagCount] FROM [TagLearning] WHERE [TagCount]>@tc OR [TagCount]=@tc ORDER BY [TagCount]", selectDocConnection);
            tagselect.Parameters.AddWithValue("@tc", mintagcount);
            SqlDataReader tagreader = tagselect.ExecuteReader();
            long tagid = 1;

            while (tagreader.Read())
            {

                if (taggeddoccount >= maxID) break;
                long taglid = (long)tagreader["TagL_ID"];
                string tagword = (string)tagreader["TagLWord"];
                double IDF = Math.Log((double)(maxID) / (double)((long)tagreader["TagCount"]));

                SqlConnection tagins = new SqlConnection(connectionstring);
                tagins.Open();
                var cmdt = new SqlCommand("INSERT INTO [Tag] ([TagWord], [IDF]) VALUES (@tagword, @idf)", tagins);
                cmdt.Parameters.AddWithValue("@tagword", tagword);
                cmdt.Parameters.AddWithValue("@idf", IDF);
                cmdt.ExecuteNonQuery();
                tagins.Close();

                SqlConnection tagindoccon = new SqlConnection(connectionstring);
                tagindoccon.Open();
                var tidcmd = new SqlCommand("SELECT [Doc_ID] FROM [TagLearningInDoc] WHERE [TagL_ID]=@ID", tagindoccon);
                tidcmd.Parameters.AddWithValue("@ID", taglid);
                SqlDataReader tidreader = tidcmd.ExecuteReader();

                while (tidreader.Read())
                {
                    taggeddoccount += mla.ArrayModify((long)tidreader[0], mintagindoccount, connectionstring);
                    tagins.Open();
                    var cmdtid = new SqlCommand("INSERT INTO [TagInDoc] ([Tag_ID], [Doc_ID]) VALUES (@tagid, @docid)", tagins);
                    cmdtid.Parameters.AddWithValue("@tagid", tagid);
                    cmdtid.Parameters.AddWithValue("@docid", (long)tidreader[0]);
                    cmdtid.ExecuteNonQuery();
                    tagins.Close();
                }

                tagindoccon.Close();
                tagid += 1;
            }
            selectDocConnection.Close();

        }//startlearningprocess
    }
}
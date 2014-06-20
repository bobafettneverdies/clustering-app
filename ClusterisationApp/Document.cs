using System.Data.SqlClient;

namespace ClusterisationApp
{
    class Document
    {
        private long docid; //идентификатор документа
        private string docbody; //текст документа
        private string[] InsignificantWords = //незначащие символы и слова
        {    ",", ".", "?", ":", "!", ";", "\n", "—", "(", ")", "{", "}", "/", "\\", "[", "]", "\"", "”", "“", "\'", "\"", " - ", "<", ">",
             " 1 ", " 2 ", " 3 ", " 4 ", " 5 ", " 6 ", " 7 ", " 8 ", " 9 ", " 0 ", 
             "n't ", "'s ", "'d ", "'ll ", "'ve ", "'re ", "'m ", 
             "n’t ", "’s ", "’d", "’ll","’ve ", "’re ", "’m ",
             " a " ,  " about " ,  " across " ,  " after " ,  " again " ,  " against " ,  " air " ,  " all " ,  " almost " ,  " along " ,  " always " ,  " am " ,  " an " , 
             " and " ,  " another " ,  " answer " ,  " any " ,  " anything " ,  " are " ,  " arm " ,  " around " ,  " as " ,  "  ask  " ,  " at " ,  " away " ,    
             " back " ,  " be " ,  " beat " ,  " because " ,  " bed " ,  " been " ,  " before " ,  " begin " ,  " behind " ,  " believe " ,  " better " ,  " big " , 
             " black " ,  " body " ,  " both " ,  " boy " ,  " break " ,  " bring " ,  " build " ,  " but " ,  " by " ,     " call " ,  " can " ,  " car " ,  " close " , 
             " come " ,  " continue " ,  " course " ,  " cut " ,     " dark " ,  " day " ,  " dead " ,  " do " ,   " down " ,  " drive " ,  " drop " ,     " each " , 
             " end " ,  " enough " ,  " even " ,  " ever " ,  " every " ,  " eye " ,     " face " ,  " fall " ,  " far " ,  " feel " ,  " feet " ,  " few " ,  " find " , 
             " fire " ,  " first " ,  " five " ,  " floor " ,  " follow " ,  " for " ,  " friend " ,  " from " ,  " front " ,     " get " ,  " girl " ,  " give " ,   " go " , 
             " good " ,  " great " ,   " guy " ,      " half " ,  " hand " ,  " happen " ,  " hard " ,  " have " ,  " he " ,  " head " ,  " hear " , 
             " help " ,  " her " ,  " here " ,  " high " ,  " him " ,  " himself " ,  " his " ,  " hold " ,  " home " ,  " hour " ,  " house " ,  " how " ,    
             " i " ,  " if " ,  " in " ,  " inside " ,  " into " ,  " is " ,  " it " ,  " its " ,     " just " ,     " keep " ,  " kid " ,   " know " ,     " last " ,  " late " , 
             " laugh " ,  " leave " ,  " let " ,  " life " ,   " like " ,  " line " ,  " little " ,  " long " ,  " look " ,  " lot " ,  " love " ,     " make " ,  " man " , 
             " may " ,  " maybe " ,  " me " ,  " mean " ,  " meet " ,  " mind " ,  " minute " ,  " moment " ,  " more " ,  " most " ,  " move " , 
             " much " ,  " must " ,  " my " ,     " name " ,  " need " ,  " never " ,  " new " ,  " next " ,  " night " ,  " no " ,  " nod " ,  " not " , 
             " nothing " ,  " now " ,  " of " ,  " off " ,   " okay " ,  " old " ,  " on " ,  " once " ,  " one " ,  " only " ,  " open " ,  " or " ,  " other " , 
             " our " ,  " out " ,  " over " ,  " own " ,     " pass " ,  " phone " ,  " pick " ,  " place " ,  " play " ,  " point " ,  " pull " ,  " put " ,     
             " reach " ,  " read " ,  " really " ,  " remember " ,  " right " ,  " room " ,  " run " ,     " same " ,  " say " ,  " second " ,  " see " , 
             " seem " ,  " set " ,  " shake " ,  " she " ,   " should " ,  " show " ,  " side " ,  " sit " ,  " small " ,  " smile " ,  " so " ,  " some " , 
             " something " ,  " sound " ,  " speak " ,  " stand " ,  " stare " ,  " start " ,  " step " ,  " still " ,  " stop " ,  " street " ,  " suddenly " , 
             " sure " ,     " table " ,  " take " ,  " talk " ,  " tell " ,  " than " ,  " thank " ,  " that " ,  " the " ,  " their " ,  " them " ,  " then " ,  " there " , 
             " these " ,  " they " ,  " thing " ,  " think " ,  " this " ,  " those " ,  " though " ,  " three " ,  " through " ,  " time " ,  " to " ,  " too " , 
             " toward " ,  " try " ,  " turn " ,  " two " ,  " towards " ,    " under " ,  " until " ,  " up " ,  " use " ,     " very " ,  " voice " ,     " wait " , 
             " walk " ,  " wall " ,  " want " ,  " was " ,  " watch " ,  " water " ,  " way " ,  " we " ,  " well " ,  " were " ,  " what " ,  " when " , 
             " where " ,  " which " ,  " while " ,  " white " ,  " who " ,  " why " ,  " will " ,  " win " ,  " window " ,  " with " ,  " without " , 
             " woman " ,  " word " ,  " work " ,  " world " ,  " would " ,     " yeah " ,  " year " ,  " yes " ,  " you " ,  " young " ,  " your ", " did ", " had ", " has", " going ", " went ", " gone ", 
             "does", "doing", "done", "people", "guys", " came ", " s "};
        
        public Document(long ID)
        {

            docid = ID;

            SqlConnection con = new SqlConnection(DBCon.Con);
            con.Open();

            var cmd = new SqlCommand("SELECT [DocBody] FROM [Doc] WHERE [Doc_ID]=@ID", con);
            cmd.Parameters.AddWithValue("@ID", ID);
            
            SqlDataReader datareader=cmd.ExecuteReader();
            
            while (datareader.Read())
                docbody = datareader[0].ToString();

            con.Close();
            
        }

        public void Normalise() //нормализовать текст документа
        {
            this.docbody=this.docbody.ToLower();
            
            for (int i = 0; i < InsignificantWords.Length; ++i)
            {
                this.docbody = this.docbody.Replace(InsignificantWords[i]," ");
            }
        }
        
        public string getbody() { return this.docbody; } //получить текст документа
    }
}

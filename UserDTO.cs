namespace UserAPI
{
    public class UserDTO
    {
        //public string id { get; set; }
        public string nome_Completo { get; set; }   
        public string identificador_Externo { get; set; }
        public string email { get; set; }
        public DateTime data_Criacao { get; set; }  
        public DateTime data_Modificacao { get; set; }
    
        //public string empresa_Padrao { get; set; }

        public string cpf { get; set; } 
        public string telefone { get; set; }    
        //public EnderecoDTO endereco_Padrao { get; set; }

        public string idioma { get; set; }

        public string status { get; set; }
        public bool flag_Deleted {  get; set; }

        public string group_Membership { get; set; }    

        public string password { get; set; }


    }
}

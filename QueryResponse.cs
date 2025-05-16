namespace UserAPI
{
    public class QueryResponse
    {
        public QueryResponse(int status, string message)
        {
            this.status  = status; 
            this.message = message;
        }

        public QueryResponse()
        {


        }

        public int status { get; set; } 
        public string message { get; set; }
        public UserDTO[] response { get; set; }

        public int current_Page { get; set; }    
        public int page_Size { get; set; }

        public int total_Rows { get; set; }

    }
}

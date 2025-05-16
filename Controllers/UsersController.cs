using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserAPI.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IEnumerable<UserDTO> _persistableUsers;   

        public UsersController() {
            _persistableUsers = Enumerable.Range(1, 2).Select(index => new UserDTO
            {
                //id = index.ToString(),
                nome_Completo = Random.Shared.Next(50).ToString(),
                identificador_Externo = Random.Shared.Next(20).ToString(),
                email = $"myemailfake{Random.Shared.Next(5)}@gmail.com",
                telefone = $"11001212{Random.Shared.Next(5)}",
                data_Modificacao = DateTime.Now,
                data_Criacao = DateTime.Now,
                status = "active",
                flag_Deleted = false,
                group_Membership = "Padrão"
            });
        }

        // GET: api/<ValuesController>
        //[HttpGet]
        private IEnumerable<UserDTO> Get()
        {
            return _persistableUsers.Where(x => x.flag_Deleted == false);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public UserDTO Get(string identificador)
        {
          

            return Get().Where(num => identificador == num.identificador_Externo).FirstOrDefault();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public QueryResponse Post([FromBody] UserDTO info)
        {
            List<UserDTO> users = new List<UserDTO>();  
            users.Add(info);

            if (info.identificador_Externo.Length == 0)
                return new QueryResponse(400, "Validation->UsersCodeLenght==0");

                Get().Append(info); 

            QueryResponse queryResponse = new QueryResponse();
            queryResponse.status = 200;
            queryResponse.message = "criado com sucesso";
            queryResponse.current_Page = 1;
            queryResponse.total_Rows = 1;
            queryResponse.page_Size = 1;
            queryResponse.response = users.ToArray();

            return queryResponse;
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string identificador, [FromBody] UserDTO info)
        {
            if (info.identificador_Externo.Length == 0)
                return BadRequest("Validation->UsersCodeLenght==0");

            UserDTO dt = Get(identificador);
            dt.nome_Completo = info.nome_Completo;

            Get().Append(dt);

            return Ok("alterado com sucesso");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string identificador)
        {
            if (identificador == "")
                return BadRequest("Validation->idnotnull");

            UserDTO dt = Get(identificador);
            dt.flag_Deleted = true;
            dt.status = "inactive";

            return Ok("excluido com sucesso");
        }


        // GET api/<ValuesController>/5
        [HttpGet("query")]
        public QueryResponse Get(string? query, int currentPage, int pageSize)
        {
            IEnumerable<UserDTO> collection = null;

            if (query != "" && query != null)
                collection = Get().Where(user => (user.identificador_Externo.Contains(query) || 
                user.nome_Completo.Contains(query) || 
                user.email.Contains(query))).ToArray();
            else
                collection = Get();


            return new QueryResponse()
            {
                status = 1,
                message = "ok",
                response = collection.Skip((currentPage - 1) * pageSize).Take(pageSize).ToArray(),
                current_Page = currentPage,
                page_Size = pageSize,
                total_Rows = collection.Count()
            };

            //if (currentPage > 0)
            //    return collection.Skip((currentPage - 1)* pageSize).Take(pageSize).ToList();
            //else
            //    return collection;
        }
    }
}

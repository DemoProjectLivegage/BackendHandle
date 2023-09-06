using Domain;

namespace Application.DTO
{
    public class COA_DTO
    {
        public int COAID { get; set; }

        public string coa_name { get; set; }
        public virtual ICollection<GL_DTO> gl_list { get; set;} 
    }
}
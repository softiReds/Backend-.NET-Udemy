namespace Backend.DTOs
{
    public class PostDTO
    {
        /*
         * Campos:
         *  int Id
         *  int UserId
         *  string Title
         *  string Body
        */

        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Title {  get; set; }
        public string? Body { get; set; }
    }
}

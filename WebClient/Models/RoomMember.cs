using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class RoomMember
    {

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string UID { get; set; }

        [MaxLength(200)]
        public string RoomName { get; set; }

        public bool InSession { get; set; } = true;

        public override string ToString()
        {
            return Name;
        }
    }
}

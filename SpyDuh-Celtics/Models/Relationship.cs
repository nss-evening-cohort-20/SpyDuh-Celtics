namespace SpyDuh_Celtics.Models
{

    public class Relationship
    {
        public int Id { get; set; }

        public User UserOne { get; set; }

        public User UserTwo { get; set; }

        public bool IsEnemy { get; set; }
    }
        public class NewRelationship
    {
        public int Id { get; set; }

        public int UserOne { get; set; }

        public int UserTwo { get; set; }

        public bool IsEnemy { get; set; }
    }

}

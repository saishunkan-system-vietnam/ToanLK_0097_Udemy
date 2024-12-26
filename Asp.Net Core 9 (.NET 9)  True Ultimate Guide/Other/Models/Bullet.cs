namespace Other.Models
{
    public class Bullet
    {
        public Bullet(int id, string name, int level)
        {
            Id = id;
            Name = name;
            this.level = level;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int level { get; set; }
    }
}

namespace _02_Travelling_Policeman
{
    public class Item
    {
        private string name;
        private int damage;
        private int count;
        private int length;

        public Item()
        {
        }

        public Item(string name, int damage, int count, int length)
        {
            this.name = name;
            this.damage = damage;
            this.count = count;
            this.length = length;
        }

        public string Name { get => this.name; set => this.name = value; }

        public int Damage { get => this.damage; set => this.damage = value; }

        public int Count { get => this.count; set => this.count = value; }

        public int Length { get => this.length; set => this.length = value; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
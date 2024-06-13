namespace ConsoleApp
{
    public class Garden
    {
        public int Size { get; }
        private ICollection<string> Items { get; }

        public Garden(int size)
        {
            Size = size;
            Items = [];
        }

        public bool Plant(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Roślina musi posiadać nazwę", nameof(name));
            }

            if (Items.Count >= Size)
            {
                return false;
            }

            if (Items.Contains(name))
            {
                //throw new ArgumentException("Taka roślina już jest w ogrodzie", nameof(name));
                name += (Items.Count(x => x.StartsWith(name)) + 1);
            }

            Items.Add(name);

            return true;
        }

        public IEnumerable<string> GetPlants()
        {
            return Items.ToList();
        }
    }
}

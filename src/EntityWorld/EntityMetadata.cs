namespace EntityWorld
{
    public class EntityMetadata
    {
        public int Generation { get; set; }

        public byte[] Instructions { get; set; }
        
        public byte[] Memory { get; set; }
    }
}
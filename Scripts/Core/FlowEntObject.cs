namespace FlowEnt
{
    public class FlowEntObject
    {
        public FlowEntObject()
        {
            Id = lastId++;
        }

        private static ulong lastId;

        public ulong Id { get; set; }
    }
}
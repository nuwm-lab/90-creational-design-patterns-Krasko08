using System;

namespace LabWork
{
    // ======== Product ========
    public class DataPacket
    {
        public string DataType { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }

        public override string ToString()
        {
            return $"Packet:\n" +
                   $"  Data Type: {DataType}\n" +
                   $"  Sender: {Sender}\n" +
                   $"  Receiver: {Receiver}";
        }
    }

    // ======== Builder Interface ========
    public interface IDataPacketBuilder
    {
        IDataPacketBuilder SetDataType(string type);
        IDataPacketBuilder SetSender(string sender);
        IDataPacketBuilder SetReceiver(string receiver);
        DataPacket Build();
    }

    // ======== Concrete Builder ========
    public class DataPacketBuilder : IDataPacketBuilder
    {
        private DataPacket packet = new DataPacket();

        public IDataPacketBuilder SetDataType(string type)
        {
            packet.DataType = type;
            return this;
        }

        public IDataPacketBuilder SetSender(string sender)
        {
            packet.Sender = sender;
            return this;
        }

        public IDataPacketBuilder SetReceiver(string receiver)
        {
            packet.Receiver = receiver;
            return this;
        }

        public DataPacket Build()
        {
            return packet;
        }
    }

    // ======== Director ========
    public class PacketDirector
    {
        public DataPacket BuildTextPacket(IDataPacketBuilder builder)
        {
            return builder
                .SetDataType("Text")
                .SetSender("ClientA")
                .SetReceiver("Server1")
                .Build();
        }
    }

    // ======== Program ========
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new DataPacketBuilder();
            var director = new PacketDirector();

            DataPacket packet = director.BuildTextPacket(builder);

            Console.WriteLine(packet);
        }
    }
}

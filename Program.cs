using System;

namespace PacketBuilderApp
{
    // ========================= ENUMS =========================

    /// <summary>
    /// Тип пакету.
    /// </summary>
    public enum PacketType
    {
        Text,
        Binary,
        Command
    }

    /// <summary>
    /// Джерело пакету.
    /// </summary>
    public enum PacketSource
    {
        Sensor,
        User,
        System
    }

    /// <summary>
    /// Кінцевий отримувач пакету.
    /// </summary>
    public enum PacketDestination
    {
        Server,
        Database,
        LocalStorage
    }

    // ========================= PRODUCT =========================

    /// <summary>
    /// Продукт, який створюється Будівельником.
    /// Immutable.
    /// </summary>
    public sealed class Packet
    {
        public PacketType Type { get; }
        public PacketSource Source { get; }
        public PacketDestination Destination { get; }
        public string Payload { get; }
        public DateTime CreatedAt { get; }

        public Packet(PacketType type, PacketSource source, PacketDestination destination, string payload)
        {
            Type = type;
            Source = source;
            Destination = destination;
            Payload = payload;
            CreatedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Packet:\n" +
                   $"  Type: {Type}\n" +
                   $"  Source: {Source}\n" +
                   $"  Destination: {Destination}\n" +
                   $"  Payload: {Payload}\n" +
                   $"  Created: {CreatedAt}\n";
        }
    }

    // ========================= BUILDER INTERFACE =========================

    /// <summary>
    /// Інтерфейс Будівельника для покрокового створення пакету.
    /// </summary>
    public interface IPacketBuilder
    {
        void SetType(PacketType type);
        void SetSource(PacketSource source);
        void SetDestination(PacketDestination destination);
        void SetPayload(string payload);

        Packet Build();
    }

    // ========================= TEXT PACKET BUILDER =========================

    /// <summary>
    /// Конкретний Будівельник для текстових пакетів.
    /// </summary>
    public class TextPacketBuilder : IPacketBuilder
    {
        private PacketType _type;
        private PacketSource _source;
        private PacketDestination _destination;
        private string _payload = string.Empty;

        public void SetType(PacketType type) => _type = type;
        public void SetSource(PacketSource source) => _source = source;
        public void SetDestination(PacketDestination destination) => _destination = destination;

        public void SetPayload(string payload)
        {
            _payload = payload;
        }

        public Packet Build()
        {
            return new Packet(_type, _source, _destination, _payload);
        }
    }

    // ========================= BINARY PACKET BUILDER =========================

    /// <summary>
    /// Будівельник, який автоматично кодує дані в Base64.
    /// </summary>
    public class DataPacketBuilder : IPacketBuilder
    {
        private PacketType _type;
        private PacketSource _source;
        private PacketDestination _destination;
        private string _payload = string.Empty;

        public void SetType(PacketType type) => _type = type;
        public void SetSource(PacketSource source) => _source = source;
        public void SetDestination(PacketDestination destination) => _destination = destination;

        public void SetPayload(string payload)
        {
            _payload = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(payload));
        }

        public Packet Build()
        {
            return new Packet(_type, _source, _destination, _payload);
        }
    }

    // ========================= DIRECTOR =========================

    /// <summary>
    /// Директор — створює типові конфігурації пакетів.
    /// </summary>
    public class PacketDirector
    {
        public Packet BuildStatusPacket(IPacketBuilder builder, string message)
        {
            builder.SetType(PacketType.Text);
            builder.SetSource(PacketSource.System);
            builder.SetDestination(PacketDestination.Server);
            builder.SetPayload(message);

            return builder.Build();
        }

        public Packet BuildSensorPacket(IPacketBuilder builder, string data)
        {
            builder.SetType(PacketType.Binary);
            builder.SetSource(PacketSource.Sensor);
            builder.SetDestination(PacketDestination.Database);
            builder.SetPayload(data);

            return builder.Build();
        }
    }

    // ========================= DEMO =========================

    internal class Program
    {
        static void Main(string[] args)
        {
            var director = new PacketDirector();

            // Текстовий пакет (Concrete Builder)
            IPacketBuilder textBuilder = new TextPacketBuilder();
            Packet statusPacket = director.BuildStatusPacket(textBuilder, "System operational.");
            Console.WriteLine(statusPacket);

            // Бінарний пакет (Concrete Builder)
            IPacketBuilder dataBuilder = new DataPacketBuilder();
            Packet sensorPacket = director.BuildSensorPacket(dataBuilder, "Temperature=23.5");
            Console.WriteLine(sensorPacket);

            Console.ReadLine();
        }
    }
}

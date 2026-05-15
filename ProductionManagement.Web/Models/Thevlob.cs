namespace ProductionManagement.Web.Models
{

    public class Thevlob
    {
        public DateTime EnqueuedTimeUtc { get; set; }
        public Properties Properties { get; set; }
        public Systemproperties SystemProperties { get; set; }
        public Body Body { get; set; }
    }

    public class Properties
    {
    }

    public class Systemproperties
    {
        public string connectionDeviceId { get; set; }
        public string connectionAuthMethod { get; set; }
        public string connectionDeviceGenerationId { get; set; }
        public string contentType { get; set; }
        public string contentEncoding { get; set; }
        public DateTime enqueuedTime { get; set; }
    }

    public class Body
    {
        public string data { get; set; }
        public string device_id { get; set; }
        public string @event { get; set; }
        public DateTime published_at { get; set; }
        public int fw_version { get; set; }
    }

    //
    public class MeasuredData
    {
        public float Tfah { get; set; }
        public int Hum { get; set; }
    }
    //

    public class MoreData 
    {
        public MeasuredData MeasuredData { get; set; }
        public Body Body { get; set; }
    }
}

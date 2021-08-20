namespace BindNineCore.Config
{
    public class BindConfig
    {
        public string BindPath { get; set; } = "/etc/bind";
        public string BindPathConfigOverride { get; set; }
        public string ConnectionString { get; set; }
        public string DefaultNameserver { get; set; } = "172.16.253.1"; 
        
        public PostBuildConfig[] PostBuild { get; set; }
    }

    public class PostBuildConfig
    {
        public string Bin { get; set; }
        public string Args { get; set; }
    }
}
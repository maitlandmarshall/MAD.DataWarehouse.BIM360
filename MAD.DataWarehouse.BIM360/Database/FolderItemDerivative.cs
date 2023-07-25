using MAD.DataWarehouse.BIM360.Api.Accounts;

namespace MAD.DataWarehouse.BIM360.Database
{
    internal class FolderItemDerivative
    {
        public string ProjectId { get; set; }
        public string FolderItemId { get; set; }

        public string Data { get; set; }

        public string RVTVersion { get; set; }

        public virtual Project Project { get; set; }
    }
}

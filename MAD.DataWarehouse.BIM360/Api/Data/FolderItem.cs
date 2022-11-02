using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MAD.DataWarehouse.BIM360.Api.Data
{
    public class FolderItem
    {
        public string ProjectId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("attributes")]
        public FolderItemAttribute Attributes { get; set; }

        [JsonProperty("relationships")]
        public FolderItemRelationships Relationships { get; set; }
    }

    public partial class FolderItemRelationships
    {
        [JsonProperty("parent")]
        public RelationshipContainer Parent { get; set; }

        [JsonProperty("tip")]
        public RelationshipContainer Tip { get; set; }
        
        [JsonProperty("storage")]
        public RelationshipContainer Storage { get; set; }

        [JsonProperty("item")]
        public RelationshipContainer Item { get; set; }
    }

    public partial class RelationshipContainer
    {
        [JsonProperty("data")]
        public RelationshipContainerData Data { get; set; }

        [JsonProperty("meta")]
        public RelationshipContainerMeta Meta { get; set; }
    }

    public partial class RelationshipContainerData
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class RelationshipContainerMetaLink
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class RelationshipContainerMeta
    {
        [JsonProperty("link")]
        public RelationshipContainerMetaLink Link { get; set; }
    }

    public partial class FolderItemAttribute
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("createTime")]
        public DateTimeOffset CreateTime { get; set; }

        [JsonProperty("createUserId")]
        public string CreateUserId { get; set; }

        [JsonProperty("createUserName")]
        public string CreateUserName { get; set; }

        [JsonProperty("lastModifiedTime")]
        public DateTimeOffset LastModifiedTime { get; set; }

        [JsonProperty("lastModifiedUserId")]
        public string LastModifiedUserId { get; set; }

        [JsonProperty("lastModifiedUserName")]
        public string LastModifiedUserName { get; set; }

        [JsonProperty("lastModifiedTimeRollup")]
        public DateTimeOffset LastModifiedTimeRollup { get; set; }

        [JsonProperty("objectCount")]
        public long ObjectCount { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("extension")]
        public FolderItemAttributeExtension Extension { get; set; }

        [JsonProperty("versionNumber")]
        public int? VersionNumber { get; set; }

        [JsonProperty("mimeType")]
        public string MimeType { get; set; }

        [JsonProperty("storageSize")]
        public int? StorageSize { get; set; }

        [JsonProperty("fileType")]
        public string FileType { get; set; }
    }

    public partial class FolderItemAttributeExtension
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("schema")]
        public FolderItemAttributeExtensionDataSchema Schema { get; set; }

        [JsonProperty("data")]
        public FolderItemAttributeExtensionData Data { get; set; }
    }

    public partial class FolderItemAttributeExtensionData
    {
        [JsonProperty("visibleTypes")]
        public List<string> VisibleTypes { get; set; }

        [JsonProperty("actions")]
        public List<string> Actions { get; set; }

        [JsonProperty("allowedTypes")]
        public List<string> AllowedTypes { get; set; }

        [JsonProperty("namingStandardIds")]
        public List<object> NamingStandardIds { get; set; }
    }

    public partial class FolderItemAttributeExtensionDataSchema
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }
    }
}

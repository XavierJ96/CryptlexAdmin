using System;
using System.Collections.Generic;

namespace CryplexAdmin.Models
{
    public class ProductResponse
    {
        public string Id { get; set; } = null;
        public DateTime? CreatedAt { get; set; } = null;
        public DateTime? UpdatedAt { get; set; } = null;
        public string Name { get; set; } = null;
        public string DisplayName { get; set; } = null;
        public string Description { get; set; } = null;
        public string PublicKey { get; set; } = null;
        public int? TotalLicenses { get; set; } = null;
        public int? TotalTrialActivations { get; set; } = null;
        public int? TotalReleases { get; set; } = null;
        public int? TotalProductVersions { get; set; } = null;
        public int? TotalFeatureFlags { get; set; } = null;
        public List<string> AutomatedEmails { get; set; } = null;
        public string LicenseTemplateId { get; set; } = null;
        public string TrialPolicyId { get; set; } = null;
        public List<MetadataDto> Metadata { get; set; } = null;
        public bool IsSelected { get; set; }
    }

    public class MetadataDto
    {
        public string Id { get; set; } = null;
        public DateTime? CreatedAt { get; set; } = null;
        public DateTime? UpdatedAt { get; set; } = null;
        public string Key { get; set; } = null;
        public string Value { get; set; } = null;
        public bool Visible { get; set; }
    }
}

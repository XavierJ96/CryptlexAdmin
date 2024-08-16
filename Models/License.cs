using System;

namespace CryplexAdmin.Models
{
    public class License
    {
        public string Key { get; set; } = null;
        public bool? Revoked { get; set; } = null;
        public bool? Suspended { get; set; } = null;
        public int? TotalActivations { get; set; } = null;
        public int? TotalDeactivations { get; set; } = null;
        public int? Validity { get; set; } = null;
        public string ExpirationStrategy { get; set; } = null;
        public string FingerprintMatchingStrategy { get; set; } = null;
        public int? AllowedActivations { get; set; } = null;
        public int? AllowedDeactivations { get; set; } = null;
        public string Type { get; set; } = null;
        public int? AllowedFloatingClients { get; set; } = null;
        public int? ServerSyncGracePeriod { get; set; } = null;
        public int? ServerSyncInterval { get; set; } = null;
        public int? AllowedClockOffset { get; set; } = null;
        public bool? DisableClockValidation { get; set; } = null;
        public int? ExpiringSoonEventOffset { get; set; } = null;
        public int? LeaseDuration { get; set; } = null;
        public string LeasingStrategy { get; set; } = null;
        public DateTime? ExpiresAt { get; set; } = null;
        public bool? AllowVmActivation { get; set; } = null;
        public bool? AllowContainerActivation { get; set; } = null;
        public bool? AllowClientLeaseDuration { get; set; } = null;
        public bool? UserLocked { get; set; } = null;
        public bool? RequireAuthentication { get; set; } = null;
        public bool? DisableGeoLocation { get; set; } = null;
        public string Notes { get; set; } = null;
        public string ProductId { get; set; } = null;
        public object ProductVersionId { get; set; } = null;
        public object MaintenancePolicyId { get; set; } = null;
        public object MaintenanceExpiresAt { get; set; } = null;
        public object CurrentReleaseVersion { get; set; } = null;
        public string MaxAllowedReleaseVersion { get; set; } = null;
        public object Organization { get; set; } = null;
        public object User { get; set; } = null;
        public object Reseller { get; set; } = null;
        public object[] AdditionalUserIds { get; set; } = null;
        public object AllowedIpRange { get; set; } = null;
        public object[] AllowedIpRanges { get; set; } = null;
        public object[] AllowedIpAddresses { get; set; } = null;
        public object[] DisallowedIpAddresses { get; set; } = null;
        public object[] AllowedCountries { get; set; } = null;
        public object[] DisallowedCountries { get; set; } = null;
        public object[] Metadata { get; set; } = null;
        public object[] MeterAttributes { get; set; } = null;
        public object[] Tags { get; set; } = null;
        public object[] ExternalUserIds { get; set; } = null;
        public string Id { get; set; } = null;
        public DateTime? CreatedAt { get; set; } = null;
        public DateTime? UpdatedAt { get; set; } = null;
        public bool IsSelected { get; set; }
    }
}

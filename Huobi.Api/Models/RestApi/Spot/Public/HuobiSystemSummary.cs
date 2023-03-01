namespace Huobi.Api.Models.RestApi.Spot.Public;

public class HuobiSystemSummary
{
    [JsonProperty("page")]
    public HuobiSystemSummaryPage Page { get; set; }

    [JsonProperty("components")]
    public IEnumerable<HuobiSystemComponent> Components { get; set; }

    [JsonProperty("incidents")]
    public IEnumerable<HuobiSystemIncident> Incidents { get; set; }

    [JsonProperty("scheduled_maintenances")]
    public IEnumerable<HuobiSystemIncident> ScheduledMaintenances { get; set; }

    [JsonProperty("status")]
    public HuobiSystemSummaryStatus Status { get; set; }
}

public class HuobiSystemSummaryPage
{
    [JsonProperty("id")]
    public string PageId { get; set; }

    [JsonProperty("name")]
    public string PageName { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("time_zone")]
    public string TimeZone { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }
}

public class HuobiSystemComponent
{
    [JsonProperty("id")]
    public string ComponentId { get; set; }

    [JsonProperty("name")]
    public string ComponentName { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("position")]
    public int Position { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("showcase")]
    public bool Showcase { get; set; }

    [JsonProperty("group_id")]
    public string GroupId { get; set; }

    [JsonProperty("page_id")]
    public string PageId { get; set; }

    [JsonProperty("group")]
    public bool Group { get; set; }

    [JsonProperty("only_show_if_degraded")]
    public bool OnlyShowIfDegraded { get; set; }
}

public class HuobiSystemIncident
{
    [JsonProperty("id")]
    public string IncidentId { get; set; }

    [JsonProperty("name")]
    public string IncidentName { get; set; }
    
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("started_at")]
    public DateTime? StartedAt { get; set; }
    
    [JsonProperty("monitoring_at")]
    public DateTime? MonitoringAt { get; set; }

    [JsonProperty("rersolved_at")]
    public DateTime? ResolvedAt { get; set; }

    [JsonProperty("impact")]
    public string Impact { get; set; }
    
    [JsonProperty("shortlink")]
    public string Shortlink { get; set; }
    
    [JsonProperty("page_id")]
    public string PageId { get; set; }
    
    [JsonProperty("incident_updates")]
    public IEnumerable<HuobiSystemIncidentUpdate> Updates { get; set; }
}

public class HuobiSystemIncidentUpdate
{
    [JsonProperty("id")]
    public string IncidentUpdateId { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("body")]
    public string Body { get; set; }

    [JsonProperty("incident_id")]
    public string IncidentId { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("display_at")]
    public DateTime DisplayAt { get; set; }

    [JsonProperty("affected_components")]
    public IEnumerable<HuobiSystemIncidentAffectedComponent> AffectedComponents { get; set; }

    [JsonProperty("deliver_notifications")]
    public bool deliver_notifications { get; set; }

    //[JsonProperty("custom_tweet")]
    //public string custom_tweet { get; set; }

    //[JsonProperty("tweet_id")]
    //public string tweet_id { get; set; }
}

public class HuobiSystemIncidentAffectedComponent
{
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("old_status")]
    public string OldStatus { get; set; }

    [JsonProperty("new_status")]
    public string NewStatus { get; set; }
}

public class HuobiSystemSummaryStatus
{
    [JsonProperty("indicator")]
    public string Indicator { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }
}
namespace CollegeFestMVC.Extensions;

public static class ParticipantExtensions
{
    /// <summary>
    /// Returns the registration state used by the Index and Details views.
    /// </summary>
    public static string GetRegistrationStatus(this Participant participant)
    {
        return string.IsNullOrWhiteSpace(participant.ParticipantName) ||
               string.IsNullOrWhiteSpace(participant.Email) ||
               string.IsNullOrWhiteSpace(participant.EventName)
            ? "Pending"
            : "Confirmed";
    }

    /// <summary>
    /// Categorises an event fee as requested by the task.
    /// </summary>
    public static string GetFeeCategory(this Event eventItem)
    {
        return eventItem.RegistrationFee switch
        {
            0 => "Free Event",
            <= 500 => "Standard Event",
            _ => "Premium Event"
        };
    }

    /// <summary>
    /// Returns a Bootstrap contextual class for the registration status.
    /// </summary>
    public static string GetStatusBadgeClass(this Participant participant)
    {
        return participant.GetRegistrationStatus() == "Confirmed"
            ? "bg-success"
            : "bg-warning text-dark";
    }
}

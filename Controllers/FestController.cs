using CollegeFestMVC.Models;

namespace CollegeFestMVC.Controllers;

public class FestController : Controller
{
    private static readonly List<Event> Events =
    [
        new Event { EventId = 1, EventName = "Hackathon", MaximumParticipants = 100, RegistrationFee = 0 },
        new Event { EventId = 2, EventName = "Coding Challenge", MaximumParticipants = 80, RegistrationFee = 250 },
        new Event { EventId = 3, EventName = "Tech Quiz", MaximumParticipants = 120, RegistrationFee = 150 },
        new Event { EventId = 4, EventName = "Robo Wars", MaximumParticipants = 60, RegistrationFee = 750 },
        new Event { EventId = 5, EventName = "Paper Presentation", MaximumParticipants = 50, RegistrationFee = 600 }
    ];

    // The task explicitly asks for temporary storage in a collection.
    private static readonly List<Participant> Participants =
    [
        new Participant
        {
            ParticipantId = 1,
            ParticipantName = "Aarav Shah",
            Email = "aarav@example.com",
            Department = "Computer Engineering",
            Year = 3,
            EventName = "Hackathon",
            IsTeamEvent = true
        }
    ];

    public IActionResult Index()
    {
        ViewBag.Events = Events;
        return View(Participants);
    }

    [HttpGet]
    public IActionResult Register()
    {
        ViewBag.Events = Events;
        return View(new Participant());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(Participant participant)
    {
        ViewBag.Events = Events;

        var selectedEvent = Events.FirstOrDefault(e =>
            e.EventName.Equals(participant.EventName, StringComparison.OrdinalIgnoreCase));

        if (selectedEvent is null)
        {
            ModelState.AddModelError(nameof(participant.EventName), "Please select a valid event.");
        }
        else
        {
            var registeredCount = Participants.Count(p =>
                p.EventName.Equals(selectedEvent.EventName, StringComparison.OrdinalIgnoreCase));

            if (registeredCount >= selectedEvent.MaximumParticipants)
            {
                ModelState.AddModelError(nameof(participant.EventName), "This event has reached its maximum capacity.");
            }
        }

        if (!ModelState.IsValid)
        {
            return View(participant);
        }

        participant.ParticipantId = Participants.Count == 0
            ? 1
            : Participants.Max(p => p.ParticipantId) + 1;

        Participants.Add(participant);
        TempData["RegistrationMessage"] =
            $"Registration confirmed for {participant.ParticipantName} for the {participant.EventName} event.";

        return RedirectToAction(nameof(Details), new { id = participant.ParticipantId });
    }

    public IActionResult Details(int id)
    {
        var participant = Participants.FirstOrDefault(p => p.ParticipantId == id);

        if (participant is null)
        {
            return NotFound();
        }

        ViewBag.Event = Events.FirstOrDefault(e =>
            e.EventName.Equals(participant.EventName, StringComparison.OrdinalIgnoreCase));

        return View(participant);
    }

    public IActionResult Welcome()
    {
        ViewBag.EventCount = Events.Count;
        ViewBag.RegistrationCount = Participants.Count;
        return View();
    }
}

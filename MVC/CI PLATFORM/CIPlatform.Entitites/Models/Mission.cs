﻿using System;
using System.Collections.Generic;

namespace CIPlatform.Entitites.Models;

public partial class Mission
{
    public long MissionId { get; set; }

    public long? ThemeId { get; set; }

    public long CityId { get; set; }

    public long CountryId { get; set; }

    public string Title { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string MissionType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? OrganizationName { get; set; }

    public string? OrganizationDetail { get; set; }

    public string? Availability { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? GoalObject { get; set; }

    public long? TotalSeats { get; set; }

    public long? AvbSeat { get; set; }

    public long? Achieved { get; set; }

    public DateTime? Deadline { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<FavoriteMission> FavoriteMissions { get; } = new List<FavoriteMission>();

    public virtual ICollection<GoalMission> GoalMissions { get; } = new List<GoalMission>();

    public virtual ICollection<MissionApplication> MissionApplications { get; } = new List<MissionApplication>();

    public virtual ICollection<MissionDocument> MissionDocuments { get; } = new List<MissionDocument>();

    public virtual ICollection<MissionMedium> MissionMedia { get; } = new List<MissionMedium>();

    public virtual ICollection<MissionRating> MissionRatings { get; } = new List<MissionRating>();

    public virtual ICollection<MissionSkill> MissionSkills { get; } = new List<MissionSkill>();

    public virtual ICollection<Story> Stories { get; } = new List<Story>();

    public virtual MissionTheme? Theme { get; set; }

    public virtual ICollection<Timesheet> Timesheets { get; } = new List<Timesheet>();
}

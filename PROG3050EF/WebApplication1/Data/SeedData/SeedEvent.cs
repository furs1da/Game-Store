using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.SeedData
{
    public class SeedEvent : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> entity)
        {
            entity.HasData(
                new Event { EventId = 1, Name = "DevGAMM Game Fest 2022", Date = new DateTime(2022, 11, 25), Description = "Join DevGAMM Game Fest 2022, that takes place online on November 23-25 and will be fully dedicated to games and devs.", Duration= "5 hours" },
                new Event { EventId = 2, Name = "Game Wave Festival", Date = new DateTime(2022, 11, 28), Description = "The Game Wave Festival is a business, education and entertainment event for the gaming industry professionals.", Duration = "2 hours" },
                new Event { EventId = 3, Name = "Africa Games Week 2022", Date = new DateTime(2022, 12, 1), Description = "The largest gathering of African game developers, exporters and leaders in the world and this year is pushing the boundaries to make sure the African industry is meeting and building a community to develop the industry.", Duration = "3 hours" },
                new Event { EventId = 4, Name = "Reboot Develop Red 2022", Date = new DateTime(2022, 12, 7), Description = "Second year of the Canadian edition of world renown Reboot Develop boutique games industry and game developers conference, famous for unbeatable speaker lineups and huge attendance.", Duration = "4 hours" },
                new Event { EventId = 5, Name = "Taipei Game Show 2022", Date = new DateTime(2022, 12, 9), Description = "The 2023 event will run on a hybrid model, encompassing both in-person and online-only events, and the theme this year will be Come in Gameverse", Duration = "7 hours" },
                new Event { EventId = 6, Name = "Reboot Develop Blue 2022", Date = new DateTime(2022, 12, 15), Description = "The renowned Croatian developer conference returns to its usual April slot, once again inviting game makers and industry professionals to Dubrovnik. ", Duration = "8 hours" },
                new Event { EventId = 7, Name = "Develop Brighton 2022", Date = new DateTime(2022, 12, 24), Description = "Some Description.", Duration = "12 hours" },
                new Event { EventId = 8, Name = "EGX 2023", Date = new DateTime(2022, 12, 28), Description = "The UK’s largest video games event, drawing tens of thousands of visitors to a four-day celebration of all things gaming.", Duration = "2 hours" },
                new Event { EventId = 9, Name = "Awesome Games Done Quick", Date = new DateTime(2022, 12, 30), Description = "Games Done Quick announced that Awesome Games Done Quick 2022, supporting " +
                "Prevent Cancer Foundation, will take place entirely online.", Duration = "7 hours" }
            );
        }
    }
}

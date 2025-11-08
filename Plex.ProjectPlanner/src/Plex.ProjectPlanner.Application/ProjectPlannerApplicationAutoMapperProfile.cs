using AutoMapper;
using Plex.ProjectPlanner.Books;
using Plex.ProjectPlanner.Settings;

namespace Plex.ProjectPlanner;

public class ProjectPlannerApplicationAutoMapperProfile : Profile
{
    public ProjectPlannerApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<ApplicationSetting, ApplicationSettingDto>();
        CreateMap<CreateUpdateApplicationSettingDto, ApplicationSetting>();
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}

using AutoMapper;
using Plex.ProjectPlanner.Books;

namespace Plex.ProjectPlanner;

public class ProjectPlannerApplicationAutoMapperProfile : Profile
{
    public ProjectPlannerApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}

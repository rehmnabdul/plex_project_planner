using AutoMapper;
using Plex.ProjectPlanner.Books;

namespace Plex.ProjectPlanner.Web;

public class ProjectPlannerWebAutoMapperProfile : Profile
{
    public ProjectPlannerWebAutoMapperProfile()
    {
        CreateMap<BookDto, CreateUpdateBookDto>();
        
        //Define your object mappings here, for the Web project
    }
}

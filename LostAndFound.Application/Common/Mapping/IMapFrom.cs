using AutoMapper;
using Intent.RoslynWeaver.Attributes;

namespace LostAndFound.Application.Common.Mappings
{
    interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}
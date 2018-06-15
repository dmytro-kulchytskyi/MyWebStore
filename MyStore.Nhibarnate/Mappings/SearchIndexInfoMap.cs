using FluentNHibernate.Mapping;
using MyStore.Business.Entities;
using MyStore.Business.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate.Mappings
{
    public class SearchIndexInfoMap : ClassMap<SearchIndexInfo>
    {
        public SearchIndexInfoMap()
        {
            Table("SearchIndexInfo");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.Date).Not.Nullable();
            Map(x => x.IndexErrorMessage).Nullable();
            Map(x => x.IndexErrorStackTrace).Nullable();
            Map(x => x.IndexFilesLocation).Nullable();
            Map(x => x.IndexFinished).Not.Nullable();
            Map(x => x.IndexInProgress).Not.Nullable();
            Map(x => x.IndexSuccess).Not.Nullable();
        }
    }
}

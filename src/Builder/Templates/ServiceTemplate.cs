using Microsoft.EntityFrameworkCore;
using Project.Business.Common.Data;
using Project.Domain.Entities;

namespace Project.Business.Services.{{EntityPluralName}};

public class {{EntityName}}Services(Context context)
{
    public async Task<List<{{EntityName}}>> GetAll{{EntityPluralName}}()
    {
        return await context.{{EntityPluralName}}.ToListAsync();
    }
    // TODO: Add service methods
}

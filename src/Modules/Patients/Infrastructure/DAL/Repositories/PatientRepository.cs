using CompanyName.MyProjectName.Modules.Patients.Domain.Patients.Entities;
using CompanyName.MyProjectName.Modules.Patients.Domain.Patients.Repositories;
using CompanyName.MyProjectName.Modules.Patients.Domain.Patients.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyProjectName.Modules.Patients.Infrastructure.DAL.Repositories;

internal class PatientRepository : IPatientRepository
{
    private readonly PatientsDbContext _context;
    private readonly DbSet<Patient> _patients;

    public PatientRepository(PatientsDbContext context)
    {
        _context = context;
        _patients = _context.Patients;
    }

    public Task<Patient> GetAsync(PatientId id, CancellationToken cancellationToken = default)
    {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        return _patients.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
    }

    public async Task AddAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        await _patients.AddAsync(patient, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        _patients.Update(patient);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
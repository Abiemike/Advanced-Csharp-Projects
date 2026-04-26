//using CareerCloud.ADODataAccessLayer;
using CareerCloud.ADODataAccessLayer;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register repositories
builder.Services.AddScoped<IDataRepository<ApplicantEducationPoco>, ApplicantEducationRepository>();
builder.Services.AddScoped<IDataRepository<ApplicantJobApplicationPoco>, ApplicantJobApplicationRepository>();
builder.Services.AddScoped<IDataRepository<ApplicantProfilePoco>, ApplicantProfileRepository>();
builder.Services.AddScoped<IDataRepository<ApplicantResumePoco>, ApplicantResumeRepository>();
builder.Services.AddScoped<IDataRepository<ApplicantSkillPoco>, ApplicantSkillRepository>();
builder.Services.AddScoped<IDataRepository<ApplicantWorkHistoryPoco>, ApplicantWorkHistoryRepository>();
builder.Services.AddScoped<IDataRepository<CompanyDescriptionPoco>, CompanyDescriptionRepository>();
builder.Services.AddScoped<IDataRepository<CompanyJobDescriptionPoco>, CompanyJobDescriptionRepository>();
builder.Services.AddScoped<IDataRepository<CompanyJobEducationPoco>, CompanyJobEducationRepository>();
builder.Services.AddScoped<IDataRepository<CompanyJobPoco>, CompanyJobRepository>();
builder.Services.AddScoped<IDataRepository<CompanyJobSkillPoco>, CompanyJobSkillRepository>();
builder.Services.AddScoped<IDataRepository<CompanyLocationPoco>, CompanyLocationRepository>();
builder.Services.AddScoped<IDataRepository<CompanyProfilePoco>, CompanyProfileRepository>();
builder.Services.AddScoped<IDataRepository<SecurityLoginPoco>, SecurityLoginRepository>();
builder.Services.AddScoped<IDataRepository<SecurityLoginsLogPoco>, SecurityLoginsLogRepository>();
builder.Services.AddScoped<IDataRepository<SecurityLoginsRolePoco>, SecurityLoginsRoleRepository>();
builder.Services.AddScoped<IDataRepository<SecurityRolePoco>, SecurityRoleRepository>();
builder.Services.AddScoped<IDataRepository<SystemCountryCodePoco>, SystemCountryCodeRepository>();
builder.Services.AddScoped<IDataRepository<SystemLanguageCodePoco>, SystemLanguageCodeRepository>();

//Register logic classes

//Add controllers
builder.Services.AddControllers();
builder.Services.AddScoped<ApplicantEducationLogic>();
builder.Services.AddScoped<ApplicantJobApplicationLogic>();
builder.Services.AddScoped<ApplicantProfileLogic>();
builder.Services.AddScoped<ApplicantResumeLogic>();
builder.Services.AddScoped<ApplicantSkillLogic>();
builder.Services.AddScoped<ApplicantWorkHistoryLogic>();
builder.Services.AddScoped<CompanyDescriptionLogic>();
builder.Services.AddScoped<CompanyJobDescriptionLogic>();
builder.Services.AddScoped<CompanyJobEducationLogic>();
builder.Services.AddScoped<CompanyJobLogic>();
builder.Services.AddScoped<CompanyJobSkillLogic>();
builder.Services.AddScoped<CompanyLocationLogic>();
builder.Services.AddScoped<CompanyProfileLogic>();
builder.Services.AddScoped<SecurityLoginLogic>();
builder.Services.AddScoped<SecurityLoginsLogLogic>();
builder.Services.AddScoped<SecurityLoginsRoleLogic>();
builder.Services.AddScoped<SecurityRoleLogic>();
builder.Services.AddScoped<SystemCountryCodeLogic>();
builder.Services.AddScoped<SystemLanguageCodeLogic>();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

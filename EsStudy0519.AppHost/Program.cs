using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("azurestorage")
    // .RunAsEmulator(opt => opt.WithDataVolume());
    .RunAsEmulator();
var clusteringTable = storage.AddTables("EsStudy0519Clustering");
var grainTable = storage.AddTables("EsStudy0519GrainTable");
var grainStorage = storage.AddBlobs("EsStudy0519GrainState");
var queue = storage.AddQueues("EsStudy0519Queue");



var postgres = builder
    .AddPostgres("orleansSekibanPostgres")
    // .WithDataVolume() // Uncomment to use a data volume
    .WithPgAdmin()
    .AddDatabase("SekibanPostgres");

var orleans = builder.AddOrleans("default")
    .WithClustering(clusteringTable)
    .WithGrainStorage("Default", grainStorage)
    .WithGrainStorage("EsStudy0519Queue", grainStorage)
    .WithGrainStorage("EsStudy0519GrainTable", grainTable)
    .WithStreaming(queue);

var apiService = builder.AddProject<EsStudy0519_ApiService>("apiservice")
    // .WithEndpoint("https", annotation => annotation.IsProxied = false)
    .WithReference(postgres)
    .WithReference(orleans)
    // .WithReplicas(2); // Uncomment to run with 2 replicas
    ;
builder.AddProject<Projects.EsStudy0519_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();

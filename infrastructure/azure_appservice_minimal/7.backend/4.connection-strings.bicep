param appServiceName string = 'backend-${resourceGroup().name}'
@description('The name of the Key Vault.')
param keyVaultName string = 'kv-${resourceGroup().name}'

@description('Database type to use (cosmos or postgres)')
@allowed(['cosmos', 'postgres'])
param databaseType string = 'cosmos'

param orleansClusterType string = 'cosmos'

@description('Queue type for Orleans')
param orleansQueueType string = 'azurestorage' //'eventhub'

// Database connection string parameter names (for application settings)
var databaseConnectionStringName = databaseType == 'postgres' 
  ? 'SekibanPostgres' 
  : 'SekibanCosmos'
var databaseConnectionStringSecretName = databaseType == 'postgres' 
  ? 'SekibanPostgresConnectionString' 
  : 'SekibanCosmosDbConnectionString'

var orleansClusteringConnectionStringName = orleansClusterType == 'cosmos' 
  ? 'OrleansCosmos' 
  : 'EsStudy0519Clustering'
var orleansClusteringConnectionStringSecretName = orleansClusterType == 'cosmos' 
  ? 'SekibanCosmosDbConnectionString' 
  : 'OrleansClusteringConnectionString'


var orleansGrainStateConnectionStringSecretName = 'OrleansGrainStateConnectionString'
var orleansQueueConnectionStringSecretName = orleansQueueType == 'eventhub'
  ? 'EventHubConnectionString'
  : 'OrleansQueueConnectionString'
var orleansQueueConnectionStringName = orleansQueueType == 'eventhub'
  ? 'OrleansEventHub'
  : 'EsStudy0519Queue'
// Reference to the existing App Service
resource webApp 'Microsoft.Web/sites@2022-09-01' existing = {
  name: appServiceName
}

// Update the App Service with connection strings
resource connectionStringsConfig 'Microsoft.Web/sites/config@2022-09-01' = {
  parent: webApp
  name: 'connectionstrings'
  properties: {
    '${databaseConnectionStringName}': {
      value: '@Microsoft.KeyVault(SecretUri=https://${keyVaultName}.vault.azure.net/secrets/${databaseConnectionStringSecretName}/)'
      type: 'Custom'
    }
    '${orleansClusteringConnectionStringName}': {
      value: '@Microsoft.KeyVault(SecretUri=https://${keyVaultName}.vault.azure.net/secrets/${orleansClusteringConnectionStringSecretName}/)'
      type: 'Custom'
    }
    EsStudy0519GrainState: {
      value: '@Microsoft.KeyVault(SecretUri=https://${keyVaultName}.vault.azure.net/secrets/${orleansGrainStateConnectionStringSecretName}/)'
      type: 'Custom'
    }
    '${orleansQueueConnectionStringName}': {
      value: '@Microsoft.KeyVault(SecretUri=https://${keyVaultName}.vault.azure.net/secrets/${orleansQueueConnectionStringSecretName}/)'
      type: 'Custom'
    }
    // Use spread operator to conditionally add properties
    ...(orleansQueueType == 'eventhub' ? {
      EsStudy0519Table: {
        value: '@Microsoft.KeyVault(SecretUri=https://${keyVaultName}.vault.azure.net/secrets/OrleansClusteringConnectionString/)'
        type: 'Custom'
      }
    } : {})
  }
}

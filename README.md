## Description
OpenTelemetry.Instrumentation.MySqlData stopped working at MySql.Data@8.0.31
See: https://github.com/open-telemetry/opentelemetry-dotnet/issues/4643

I run MyApp project using MySql.Data@8.0.30 and MySql.Data@8.0.31 packages.
Output:
- [MySql.Data@8.0.30.log](./MySql.Data%408.0.30.log)
  This has Activity traces.
- [MySql.Data@8.0.31.log](./MySql.Data%408.0.31.log)
  This has no Activity traces.

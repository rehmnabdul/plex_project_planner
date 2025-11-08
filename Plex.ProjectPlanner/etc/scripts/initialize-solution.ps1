abp install-libs

cd src/Plex.ProjectPlanner.DbMigrator && dotnet run && cd -



cd src/Plex.ProjectPlanner.Web && dotnet dev-certs https -v -ep openiddict.pfx -p config.auth_server_default_pass_phrase 


exit 0
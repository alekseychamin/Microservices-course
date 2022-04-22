using System;
using System.Collections.Generic;
using System.Linq;
using CommandsService.Models;

namespace CommandsService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _contex;

        public CommandRepo(AppDbContext context)
        {
            _contex = context;
        }
        
        public void CreateCommand(int platformId, Command command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            command.PlatformId = platformId;
            _contex.Commands.Add(command);
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null) throw new ArgumentNullException(nameof(platform));

            _contex.Platforms.Add(platform);
        }

        public bool ExternalPlatformExists(int externalPlatformId)
        {
            return _contex.Platforms.Any(p => p.ExternalID == externalPlatformId);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _contex.Platforms.ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _contex.Commands.Where(c => c.PlatformId == platformId && c.Id == commandId)
                                   .SingleOrDefault();
        }

        public IEnumerable<Command> GetCommandsFroPlatfrom(int platformId)
        {
            return _contex.Commands
                .Where(c => c.PlatformId == platformId)
                .OrderBy(c => c.Platform.Name);
        }

        public bool PlatformExists(int platformId)
        {
            return _contex.Platforms.Any(p => p.Id == platformId);
        }

        public bool SaveChanges()
        {
            return (_contex.SaveChanges() >= 0);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SoftRes.Models;

namespace SoftRes.Loaders
{
    public interface IFileLoader
    {
        Dictionary<RaidInstance, string[]> GetIDs();
    }

    public class FileLoader : IFileLoader
    {
        private Dictionary<RaidInstance, string[]> _instanceToId =
            new Dictionary<RaidInstance, string[]>();
        public Dictionary<RaidInstance, string[]> GetIDs()
        {
            var idsPath = "IDs";
            var fileNames = Directory.GetFiles(idsPath)
                .Select(file => Path.GetFileName(file));
            
            foreach (var fileName in fileNames)
            {
                // TryParse outputs an object but does not return true unless the input string is part of the enum.
                // Casting object to RaidInstance is a safe assumption here.
                //
                // The name "instance" here refers to an instanced zone in World of Warcraft, not necessaryily an
                // intance of an object.
                object instance;
                if (Enum.TryParse(typeof(RaidInstance), fileName, out instance))
                {
                    _instanceToId.Add(
                        (RaidInstance) instance,
                        File.ReadAllLines(String.Join('/', idsPath, fileName))
                    );
                }
            }

            return _instanceToId;
        }
    }
}
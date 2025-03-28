﻿using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.DataLoading
{
    public class ResourceLoader : IResourceLoader
    {
        public void LoadResource<T>(string path, Type key, IRepository<T> repository) where T : class
        {
            var res = Resources.LoadAll(path, key);
            repository.Create(key, res);
        }

        public void UnloadResource<T>(Object asset, Type key, IRepository<T> repository) where T : class
        {
            Resources.UnloadAsset(asset);
            repository.Delete(key, asset as T);
        }
    }
}
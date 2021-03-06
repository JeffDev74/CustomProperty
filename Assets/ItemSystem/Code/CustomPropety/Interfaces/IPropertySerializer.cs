﻿using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
	public interface IPropertySerializer
	{
        string Serialize();
        void Deserialize<T>(string data) where T : List<Property>;
	}
}
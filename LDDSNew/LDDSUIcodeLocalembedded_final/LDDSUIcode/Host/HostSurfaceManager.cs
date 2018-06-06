using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Windows.Forms;
using System.IO;
using Loader;

namespace Host
{
	public enum LoaderType
	{
		BasicDesignerLoader = 1,
		CodeDomDesignerLoader = 2,
		NoLoader = 3
	}
    public enum GraphType
    {
        
        Curve=16,
     
        Meter=18,
      
        Report=23
        
    }
    /// <summary>
    /// Manages numerous HostSurfaces. Any services added to HostSurfaceManager
    /// will be accessible to all HostSurfaces
    /// </summary>
	public class HostSurfaceManager : DesignSurfaceManager
	{
        public BasicHostLoader basicHostLoader = null;
		public HostSurfaceManager() : base()
		{
			this.AddService(typeof(INameCreationService), new NameCreationService());
            this.ActiveDesignSurfaceChanged += new ActiveDesignSurfaceChangedEventHandler(HostSurfaceManager_ActiveDesignSurfaceChanged);
 
		}

		protected override DesignSurface CreateDesignSurfaceCore(IServiceProvider parentProvider)
		{
			return new HostSurface(parentProvider);
		}

        /// <summary>
        /// Gets a new HostSurface and loads it with the appropriate type of
        /// root component. 
        /// </summary>
        private HostControl GetNewHost(Type rootComponentType)
		{
			HostSurface hostSurface = (HostSurface)this.CreateDesignSurface(this.ServiceContainer);

			if (rootComponentType == typeof(Form))
				hostSurface.BeginLoad(typeof(Form));
			else if (rootComponentType == typeof(UserControl))
				hostSurface.BeginLoad(typeof(UserControl));
			else if (rootComponentType == typeof(Component))
				hostSurface.BeginLoad(typeof(Component));
            else
				throw new Exception("Undefined Host Type: " + rootComponentType.ToString());

			hostSurface.Initialize();
			this.ActiveDesignSurface = hostSurface;
			return new HostControl(hostSurface);
		}

        /// <summary>
        /// Gets a new HostSurface and loads it with the appropriate type of
        /// root component. Uses the appropriate Loader to load the HostSurface.
        /// </summary>
		public HostControl GetNewHost(Type rootComponentType, LoaderType loaderType)
		{
			if (loaderType == LoaderType.NoLoader)
				return GetNewHost(rootComponentType);

			HostSurface hostSurface = (HostSurface)this.CreateDesignSurface(this.ServiceContainer);
			IDesignerHost host = (IDesignerHost)hostSurface.GetService(typeof(IDesignerHost));

			switch (loaderType)
			{
				case LoaderType.BasicDesignerLoader :
					BasicHostLoader basicHostLoader = new BasicHostLoader(rootComponentType);
					hostSurface.BeginLoad(basicHostLoader);
					hostSurface.Loader = basicHostLoader;
					break;
				default:
					throw new Exception("Loader is not defined: " + loaderType.ToString());
			}

			hostSurface.Initialize();
			return new HostControl(hostSurface);
		}

        /// <summary>
        /// Opens an Xml file and loads it up using BasicHostLoader (inherits from
        /// BasicDesignerLoader)
        /// </summary>
		public HostControl GetNewHost(string fileName)
		{
			if (fileName == null || !File.Exists(fileName))
				MessageBox.Show("文件名不正确: " + fileName);

			LoaderType loaderType = LoaderType.NoLoader;

			if (fileName.EndsWith("xml"))
				loaderType = LoaderType.BasicDesignerLoader;

            if (loaderType == LoaderType.NoLoader || loaderType == LoaderType.CodeDomDesignerLoader)
            {
                throw new Exception("File cannot be opened. Please check the type or extension of the file. Supported format is Xml");
            }

			HostSurface hostSurface = (HostSurface)this.CreateDesignSurface(this.ServiceContainer);
			IDesignerHost host = (IDesignerHost)hostSurface.GetService(typeof(IDesignerHost));

            basicHostLoader = new BasicHostLoader(fileName,false);
            hostSurface.BeginLoad(basicHostLoader);
            hostSurface.Loader = basicHostLoader;
    		hostSurface.Initialize();
			return new HostControl(hostSurface);
		}
        //运行环境载入项目文件时调用的函数
        public HostControl LoadNewHost(string fileName)
        {
            if (fileName == null || !File.Exists(fileName))
                MessageBox.Show("文件名不正确: " + fileName);

            LoaderType loaderType = LoaderType.NoLoader;

            if (fileName.EndsWith("xml"))
                loaderType = LoaderType.BasicDesignerLoader;

            if (loaderType == LoaderType.NoLoader || loaderType == LoaderType.CodeDomDesignerLoader)
            {
                throw new Exception("文件打不开，请确认后缀名是xml！");
            }

            HostSurface hostSurface = (HostSurface)this.CreateDesignSurface(this.ServiceContainer);
            IDesignerHost host = (IDesignerHost)hostSurface.GetService(typeof(IDesignerHost));

            basicHostLoader = new BasicHostLoader(fileName, true);
            hostSurface.BeginLoad(basicHostLoader);
            hostSurface.Loader = basicHostLoader;
            hostSurface.Initialize();
            return new HostControl(hostSurface);
        }

		public void AddService(Type type, object serviceInstance)
		{
			this.ServiceContainer.AddService(type, serviceInstance);
		}

        /// <summary>
        /// Uses the OutputWindow service and writes out to it.
        /// </summary>
		void HostSurfaceManager_ActiveDesignSurfaceChanged(object sender, ActiveDesignSurfaceChangedEventArgs e)
		{
            //ToolWindows.OutputWindow o = this.GetService(typeof(ToolWindows.OutputWindow)) as ToolWindows.OutputWindow;
            //o.RichTextBox.Text += "New host added.\n";
		}
	}// class
}// namespace

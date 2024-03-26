using ContainerManagerApp;
using ContainerManagerApp.Controllers;
DockController controller = new DockController(new DockModel(), new DockView());
controller.Start();
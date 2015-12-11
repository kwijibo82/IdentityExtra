using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    static UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();
    static UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(userStore);
   
    static RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>();
    static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //lista de usuarios
            List<IdentityUser> userList = userManager.Users.ToList();

            GridViewMostrarUsuaris.DataSource = userList;
            //OJO! es necesario DataBind() para refrescar los datos
            GridViewMostrarUsuaris.DataBind();
        }
    }

    protected void ButtonAltaUsuarios_Click(object sender, EventArgs e)
    {

        List<IdentityUser> userList = new List<IdentityUser>();

        #region Instanciamos usuarios y añadimos valores según ejercicio
        IdentityUser usr1 = new IdentityUser();
        usr1.UserName = "adomingo";
        usr1.Email = "adomingo@cepnet.net";
        usr1.PhoneNumber = "93.445.12.67";
        userList.Add(usr1);

        IdentityUser usr2 = new IdentityUser();
        usr2.UserName = "jserrano";
        usr2.Email = "jserrano@cepnet.net";
        usr2.PhoneNumber = "93.445.12.68";
        userList.Add(usr2);

        IdentityUser usr3 = new IdentityUser();
        usr3.UserName = "csegura";
        usr3.Email = "csegura@cepnet.net";
        usr3.PhoneNumber = "93.445.12.69";
        userList.Add(usr3);

        IdentityUser usr4 = new IdentityUser();
        usr4.UserName = "umendez";
        usr4.Email = "umendez@cepnet.net";
        usr4.PhoneNumber = "93.445.12.70";
        userList.Add(usr4);

        IdentityUser usr5 = new IdentityUser();
        usr5.UserName = "amartinez";
        usr5.Email = "amartinez@cepnet.net";
        usr5.PhoneNumber = "93.445.12.71";
        userList.Add(usr5);

        IdentityUser usr6 = new IdentityUser();
        usr6.UserName = "xgalcera";
        usr6.Email = "xgalcera@cepnet.net";
        usr6.PhoneNumber = "93.445.12.72";
        userList.Add(usr6);

        IdentityUser usr7 = new IdentityUser();
        usr7.UserName = "jlgarcia";
        usr7.Email = "jlgarcia@cepnet.net";
        usr7.PhoneNumber = "93.445.12.73";
        userList.Add(usr7);
        #endregion

        //  IdentityResult resul = new IdentityResult();

        foreach (var usr in userList)
        {
            IdentityResult resul = userManager.Create(usr, "Pepe11");
            if (resul != IdentityResult.Success)
            {
                this.labelErrores.Text = resul.Errors.FirstOrDefault();
            }
        }

    }

    protected void ButtonAltaRoles_Click(object sender, EventArgs e)
    {
        
        //ROL TURNO DE MAÑANA
        IdentityRole roleMati = new IdentityRole();
        roleMati.Name = "Mati";
        roleMati.Id = "Mati";
        roleManager.Create(roleMati); //creamos el rol

        //ROL TURNO DE TARDE
        IdentityRole roleTarda = new IdentityRole();
        roleTarda.Name = "Tarda";
        roleTarda.Id = "Tarda";
        roleManager.Create(roleTarda);

        //recuperamos usuarios 
        List<IdentityUser> userList = userManager.Users.ToList();

        foreach (var user in userList)
        {
            IdentityUserRole userRole = new IdentityUserRole();
            userRole.UserId = user.Id;
            if ("jserrano".Equals(user.UserName) || "adomingo".Equals(user.UserName))
            {
                userRole.RoleId = roleMati.Id;
                roleMati.Users.Add(userRole);
            }
            else
            {
                userRole.RoleId = roleTarda.Id;
                roleTarda.Users.Add(userRole);
            }
        }
        roleManager.Update(roleMati);  // después se hace el update sobre el propio rol con el IdentityuserRol "dentro"
        roleManager.Update(roleTarda);
    }   

    //Botó 3. Modificar Usuaris (Click)  
    //Modificar el email de tots els usuaris del rol “mati”. L’email el modificarem amb el nom d’usuari + @gmai.com. 
    protected void ButtonModificarUsuarios_Click(object sender, EventArgs e)
    {
        //recuperamos usuarios 
        List<IdentityUser> userList = getUserRoles("Mati");
        IdentityResult resul = new IdentityResult();

        foreach (var user in userList)
        {
            String newName = user.UserName;
            StringBuilder str = new StringBuilder();
            str.Append(newName);
            str.Append("@gmail.com");
            user.Email = str.ToString();

            userManager.Update(user);

            if (resul != IdentityResult.Success)
            {
                this.labelErrores.Text = resul.Errors.FirstOrDefault();
            }

        }

    }

    /**
     * Recupera todos los usuarios de un determinado rol y los 
     * devuelve en una List de IdentityUser
     * */
    public List<IdentityUser> getUserRoles(String roleString)
    {
        List<IdentityRole> roleList = roleManager.Roles.ToList();
        List<IdentityUserRole> userList = new List<IdentityUserRole>();
        List<IdentityUser> completeUserList = new List<IdentityUser>(); // lista para los usuarios recuperados
        
        foreach (var rol in roleList)
        {
            if (roleString.Equals(rol.Name))
            {
                userList = rol.Users.ToList(); //recuperamos todos los usersRoles
                foreach (var user in userList)
                {
                    IdentityUser usr = new IdentityUser();
                    usr = userManager.FindById(user.UserId);
                    completeUserList.Add(usr);
                }
            }
        }

        return completeUserList;
    }

     /**
     * Devuelve en una List de IdentityRole con los roles pertenecientes a un 
      * determinado usuario
     * */
    public List<IdentityRole> getRolesFromUser(String userString)
    {
        List<IdentityUser> userList = userManager.Users.ToList();
        List<IdentityUserRole> userRoleList = new List<IdentityUserRole>();
        List<IdentityRole> completeUserRoleList = new List<IdentityRole>(); // lista para los usuarios recuperados
        
        foreach (var usr in userList)
        {
            if (userString.Equals(usr.UserName))
            {
                userRoleList = usr.Roles.ToList(); //recupera todos los roles
                foreach (var userRole in userRoleList)
                {
                    IdentityRole rol = new IdentityRole();
                    rol = roleManager.FindById(userRole.UserId);
                    completeUserRoleList.Add(rol);
                }
            }
        }

        return completeUserRoleList;
    }

    //Botó 4. Modificar Rols (Click)  
    //Agregar els usuaris “xgalcera”, “csegura” i “jlgarcia” al rol “mati”. 
    protected void ButtonModificarRoles_Click(object sender, EventArgs e)
    {
        List<IdentityUser> userList = userManager.Users.ToList();
        List<IdentityRole> roleList = roleManager.Roles.ToList();
        List<IdentityUserRole> userRoleList = new List<IdentityUserRole>();

        //ROL TURNO DE MAÑANA
        IdentityRole roleMati = roleManager.FindById("Mati");

        foreach (var usr in userList)
        {
            if (usr.UserName.Equals("xgalcera") || 
                usr.UserName.Equals("csegura") ||
                usr.UserName.Equals("jlgarcia"))
            {
                IdentityUserRole userRole = new IdentityUserRole();
                userRole.UserId = usr.Id;
                userRole.RoleId = roleMati.Id;
                userRoleList.Add(userRole);           
            }

        }

        IdentityResult resul = new IdentityResult();
        IdentityResult resul1 = new IdentityResult();
        foreach (var userRol in userRoleList)
        {
            roleMati.Users.Add(userRol);
            resul = roleManager.Update(roleMati);

            if (resul != IdentityResult.Success)
            {
                this.labelErrores.Text = resul.Errors.FirstOrDefault();
            }

        }

    }

    //Botó 5. Esborrar usuaris (Click)  
    //Esborrar tots els usuaris que estiguin al rol “mati” i al rol “tarda”.
    protected void ButtonBorrarUsuarios_Click(object sender, EventArgs e)
    {
        //NOTA: SOLO LO PRUEBO CON UN ROL; PQ SI NO ME QUEDO SIN USUARIOS

       List<IdentityUser> userListMati = getUserRoles("Mati"); //recupera usuarios del rol "Mati"
       List<IdentityUser> userListTarda = getUserRoles("Tarda"); //recupera usuarios del rol "Tarda"

       IdentityResult resul = new IdentityResult();
       foreach (var user in userListMati)
       {
           resul = userManager.Delete(user);
       }
       if (resul != IdentityResult.Success)
       {
           this.labelErrores.Text = resul.Errors.FirstOrDefault();
       }

    }
    
    //OJO: No funciona muy bien :(
    //Botó 6. Esborrar Rols (Click)  
    //Esborrar els rols que tinguin com usuari a “amartinez"
    protected void ButtonBorrarRoles_Click(object sender, EventArgs e)
    {
        List<IdentityRole> roleListToDelete = getRolesFromUser("amartinez");

        IdentityResult resul = new IdentityResult();
        foreach (var item in roleListToDelete)
        {
            resul = roleManager.Delete(item);

            if (resul != IdentityResult.Success)
            {
                this.labelErrores.Text = resul.Errors.FirstOrDefault();
            }
        }
    }

}


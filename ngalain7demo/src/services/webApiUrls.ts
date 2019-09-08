
const url_prefix = '/services/v1/';

export const webApiUrls = {
  files: {
    upload: url_prefix + 'files/upload',
    saveRelation: url_prefix + 'files/saverelation/${fileId}?businessId=${businessId}',
  },
  menus: {
    add: url_prefix + 'menus',
    delete: url_prefix + 'menus/${id}',
    update: url_prefix + 'menus/${id}',
    query: url_prefix + 'menus',
    getMenus: url_prefix + 'menus/getmenus',
    getMenusExcept: url_prefix + 'menus/getmenusexcept',
  },
  users: {
    login: url_prefix + 'users/login',
    add: url_prefix + 'users',
    delete: url_prefix + 'users/${id}',
    update: url_prefix + 'users/${id}',
    updatePassword: url_prefix + 'users/updatepassword/${id}',
    query: url_prefix + 'users',
  },
  roles: {
    add: url_prefix + 'roles',
    delete: url_prefix + 'roles/${id}',
    update: url_prefix + 'roles/${id}',
    query: url_prefix + 'roles',
  },
  roleUsers: {
    add: url_prefix + 'roleusers',
    delete: url_prefix + 'roleusers',
    update: url_prefix + 'roleusers/${id}',
    query: url_prefix + 'roleusers',
    queryNotbind: url_prefix + 'roleusers/queryisnotrole'
  },
  aclCategorys: {
    add: url_prefix + 'aclcategorys',
    delete: url_prefix + 'aclcategorys/${id}',
    update: url_prefix + 'aclcategorys/${id}',
    query: url_prefix + 'aclcategorys',
  },
  acls: {
    add: url_prefix + 'acls',
    delete: url_prefix + 'acls/${id}',
    update: url_prefix + 'acls/${id}',
  },
  objectAcls: {
    update: url_prefix + 'objectacls',
    query: url_prefix + 'objectacls',
    queryAclNames: url_prefix + 'objectacls/queryaclnames',
  }
};


export const appSettings = {
  appInfo: {
    name: `ng-alain测试项目`,
    description: `老夫聊发少年狂`,
  }
};

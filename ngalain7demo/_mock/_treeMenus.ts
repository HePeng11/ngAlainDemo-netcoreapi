import { MockRequest } from '@delon/mock';
import { deepCopy } from '@delon/util';

const list = [
  {
    text: '主导航',
    i18n: 'menu.main',
    key: 1,
    group: true,
    children: [
      {
        text: '仪表盘',
        key: 2,
        icon: 'anticon anticon-dashboard',
        children: [
          {
            text: '仪表盘1',
            key: 3,
            link: '/dashboard',
          },
        ],
      },
      {
        text: '菜单配置',
        key: 4,
        link: '/menu',
        icon: 'anticon anticon-appstore'
      }
    ],
  },
  {
    text: '测试',
    key: 5,
    group: true,
    children: [
      {
        text: '菜单配置',
        key: 6,
        i18n: 'menu.menusettree',
        link: '/menu/list1',
        icon: 'anticon anticon-appstore'
      },
      {
        text: '示例1',
        key: 7,
        i18n: 'menu.examples1',
        link: '/examples',
        icon: 'anticon anticon-appstore'
      },
      {
        text: '测试2',
        key: 8,
        group: true,
        children: [
          {
            text: '菜单配置',
            key: 9,
            i18n: 'menu.menusettree',
            link: '/menu/list1',
            icon: 'anticon anticon-appstore'
          },
          {
            text: '示例1',
            key: 10,
            i18n: 'menu.examples1',
            link: '/examples',
            icon: 'anticon anticon-appstore'
          }
        ]
      }
    ],
  }
];

function getData(params: any) {
  const pi = +params.pi,
    ps = +params.ps,
    start = (pi - 1) * ps;

  return { total: list.length, list: list.slice(start, ps * pi) };
}

function saveData(id: number, value: any) {
  const item = list.find(w => w.key === id);
  if (!item) return { msg: '无效用户信息' };
  Object.assign(item, value);
  return { msg: 'ok' };
}
function addData(value: any) {
  if (!value) return { msg: '无效用户信息' };
  value.key = list[list.length - 1].key + 1;
  list.splice(0, 0, value);
  return { msg: 'ok' };
}
function setvalue(e: any): any {
  const r = {};
  for (const key in e) {
    if (e.hasOwnProperty(key) && key !== 'children') {
      r[key] = e[key];
    }
  }
  if (e.children) {
    r['children'] = [];
    for (let i = 0; i < e.children.length; i++) {
      const element = e.children[i];
      r['children'].push(setvalue(element));
    }
  }
  return r;
}
export const treeMenus = {
  '/treeMenus': (req: MockRequest) => getData(req.queryString),
  '/services/v1/menus/getMenus': (req: MockRequest) => {
    // //Object.assign(res, list1);
    // for (let i = 0; i < list.length; i++) {
    //   const e = list[i];
    //   res.push(setvalue(e));
    // }
    const res = deepCopy(list);
    return res;
  },
  '/treeMenus/:id': (req: MockRequest) => list.find(w => w.key === +req.params.key),
  'POST /treeMenus/:id': (req: MockRequest) => saveData(+req.params.key, req.body),
  'POST /treeMenus': (req: MockRequest) => addData(req.body)
};

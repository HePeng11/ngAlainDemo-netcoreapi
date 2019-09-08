import { MockRequest } from '@delon/mock';

const list = [];
const total = 50;

for (let i = 0; i < total; i += 1) {
  list.push({
    id: i + 1,
    sdfasds: i % 6 === 0,
    disabled: i % 7 === 0,
    href: 'https://ant.design',
    avatar: [
      'https://gw.alipayobjects.com/zos/rmsportal/eeHMaZBwmTvLdIwMfBpg.png',
      'https://gw.alipayobjects.com/zos/rmsportal/udxAbMEhpwthVVcjLXik.png',
    ][i % 2],
    no: `TradeCode ${i}`,
    title: `一个任务名称 ${i}`,
    owner: '曲丽丽',
    description: '这是一段描述',
    callNo: Math.floor(Math.random() * 1000),
    status: Math.floor(Math.random() * 10) % 4,
    updatedAt: new Date(`2017-07-${Math.floor(i / 2) + 1}`),
    createdAt: new Date(`2017-07-${Math.floor(i / 2) + 1}`),
    progress: Math.ceil(Math.random() * 100),
  });
}

function genData(params: any) {
  let ret = [...list];
  const pi = +params.pi,
    ps = +params.ps,
    start = (pi - 1) * ps;

  if (params.no) {
    ret = ret.filter(data => data.no.indexOf(params.no) > -1);
  }
  if (params.description) {
    ret = ret.filter(data => data.description.indexOf(params.description) > -1);
  }
  if (params.callNo === 'ascend') {
    ret = ret.sort((a, b) => a.callNo - b.callNo);
  }
  if (params.callNo === 'descend') {
    ret = ret.sort((a, b) => b.callNo - a.callNo);
  }
  if (params.updatedAt === 'ascend') {
    ret = ret.sort((a, b) => a.updatedAt - b.updatedAt);
  }
  if (params.updatedAt === 'descend') {
    ret = ret.sort((a, b) => b.updatedAt - a.updatedAt);
  }
  return { total: ret.length, list: ret.slice(start, ps * pi) };
}

function addData(value: any) {
  if (!value) return { msg: '无效用户信息' };
  value.id = list[list.length - 1].id + 1;
  list.splice(0, 0, value);
  return { msg: 'ok' };
}

export const USERS = {
  '/user': (req: MockRequest) => genData(req.queryString),
  '/user/:id': (req: MockRequest) => list.find(w => w.id === +req.params.id),
  // 'POST /user/:id': (req: MockRequest) => saveData(+req.params.id, req.body),
  'POST /user': (req: MockRequest) => addData(req.body),
  'POST /register': {
    msg: 'ok',
  },
  'POST /services/v1/users/login': (req: MockRequest) => {
    const data = req.body;
    const users = [{
      loginname: 'admin',
      password: 'admin'
    }];
    if (!users.find(f => f.loginname === data.loginname && f.password === data.password)) {
      return {
        'msg': '账号或密码不正确',
        'code': 0
      };
    }
    return {
      'msg': '登录成功',
      'code': 1,
      'data': {
        // tslint:disable-next-line: max-line-length
        'token': 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIxIiwiUm9sZSI6ImFkbWluIiwiaWF0IjoiMjAxOS81LzI4IDExOjA5OjIwIiwiZXhwIjoxNTU5MDk5MzYwLCJpc3MiOiJIUC5HZW5lcmFsQVBJIn0.gJ9LLI1simhGqRDuF4FjE_YKb1TMZdZGbiLpt1rpxts',
        'name': 'hepeng',
        'avatar': 'https://localhost:44359/assets/tmp/img/6.png',
        'email': '18382046629'
      }
    };
  }
};

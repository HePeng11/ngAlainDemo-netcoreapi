import { MockRequest } from '@delon/mock';

export const appData = {
  '/appdata': (req: MockRequest) => {
    return {
      app: {
        name: `ng-alain测试项目`,
        description: `老夫聊发少年狂`,
      }
    };
  },
};

import { HttpParams } from '@angular/common/http';

/**
 * object toarry 转换为数组
 */
export function toarry(obj: any): Array<JsonArrayElement> {
  const ary = new Array<JsonArrayElement>();
  if (typeof (obj) === 'object') {
    for (const key in obj) {
      if (obj.hasOwnProperty(key)) {
        const element = obj[key];
        ary.push({ key, value: element });
      }
    }
  }
  return ary;
}
//  Object.prototype.toArray = function(): Array<JsonArray> {
//   const ary = new Array<JsonArray>();
//   if (typeof (this) === 'object') {
//     for (const key in this) {
//       if (this.hasOwnProperty(key)) {
//         const element = this[key];
//         ary.push({ key, value: element });
//       }
//     }
//   }
//   return ary;
// };

class JsonArrayElement {
  key: string;
  value: any;
}



/**
 * string.format()
 * @param obj string
 * @param params values
 */
export function format(obj: string, ...params: any[]): string {
  let r = obj;
  for (let index = 0; index < params.length; index++) {
    const e = params[index];
    for (const key in e) {
      if (typeof (e[key]) !== 'object') {
        r = r.replace('${' + `${key}` + '}', e[key]);
      }
    }
  }
  return r;
}

/**
 * rename param key name
 */
export function renameHttpParam(params: HttpParams | { [param: string]: string | string[]; }, name: string, newName: string) {
  params[newName] = params[name];
  delete params[name];
}

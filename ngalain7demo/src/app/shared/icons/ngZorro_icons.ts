import { IconDefinition } from '@ant-design/icons-angular';
import { ngZorro_monitorIcon, ngZorro_monitorIconName } from './monitor/monitorIcon';
import { ngZorro_homePageIcon, ngZorro_homePageIconName } from './homePage/homePageIcon';
import { ngZorro_safetyIcon, ngZorro_safetyIconName } from './safety/safety';
import { ngZorro_autherizationIcon, ngZorro_autherizationIconName } from './autherization/autherization';
import { ngZorro_keyIconName, ngZorro_keyIcon } from './key/key';
import { ngZorro_userRemoveIconName, ngZorro_userRemoveIcon } from './user-remove/user-remove';



export const ngZorro_icons: IconDefinition[] = [
  { name: ngZorro_safetyIconName, icon: ngZorro_safetyIcon, theme: 'outline' },
  { name: ngZorro_monitorIconName, icon: ngZorro_monitorIcon, theme: 'outline' },
  { name: ngZorro_homePageIconName, icon: ngZorro_homePageIcon, theme: 'outline' },
  { name: ngZorro_autherizationIconName, icon: ngZorro_autherizationIcon, theme: 'outline' },
  { name: ngZorro_keyIconName, icon: ngZorro_keyIcon, theme: 'outline' },
  { name: ngZorro_userRemoveIconName, icon: ngZorro_userRemoveIcon, theme: 'outline' },
];

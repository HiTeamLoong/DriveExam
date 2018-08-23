//  Created by 薛 龙 on 2018-8-23 20:08:01.
//
//

#import <Foundation/Foundation.h>


extern "C" {
   bool _IOS_IsInstallApp(const char *url)
    {
        if (url == NULL) {
            return false;
        }
        NSURL *nsUrl = [NSURL URLWithString:[NSString stringWithUTF8String:url]];
        if ([[UIApplication sharedApplication] canOpenURL:nsUrl]) {
            return true;
        }
        return false;
    }
}
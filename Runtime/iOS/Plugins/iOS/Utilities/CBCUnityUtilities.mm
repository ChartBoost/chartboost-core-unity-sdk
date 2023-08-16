#import "CBCUnityUtilities.h"

const char * getCStringOrNull(NSString* nsString) {
    if (nsString == NULL)
        return NULL;

    const char* nsStringUtf8 = [nsString UTF8String];
    //create a null terminated C string on the heap so that our string's memory isn't wiped out right after method's return
    char* cString = (char*)malloc(strlen(nsStringUtf8) + 1);
    strcpy(cString, nsStringUtf8);
    return cString;
}

NSString * getNSStringOrNil(const char* cString) {
    return (cString != NULL && strlen(cString)) ? [NSString stringWithUTF8String:cString] : nil;
}

NSString * getNSStringOrEmpty(const char* cString) {
    return cString != NULL ? [NSString stringWithUTF8String:cString] : @"";
}

const char* dictToJson(NSDictionary *data)
{
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:data options:0 error:&error];
    if (!jsonData) {
        NSLog(@"%s: error: %@", __func__, error.localizedDescription);
        return "";
     }
    NSString *json = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    return getCStringOrNull(json);
}

NSDictionary* stringToNSDictionary(const char* cString)
{
    NSError *error;
    NSData* jsonData = [[NSString stringWithUTF8String:cString] dataUsingEncoding:NSUTF8StringEncoding];
    
    NSDictionary* dict = [NSJSONSerialization JSONObjectWithData:jsonData options:0 error:&error];
    
    if (error != nil)
    {
        return nil;
    }

   return dict;
}


void sendToMain(block block) {
    dispatch_async(dispatch_get_main_queue(), block);
}

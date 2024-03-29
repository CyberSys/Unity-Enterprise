﻿using System.IO;

namespace UniHacker
{
    internal class UnityHubV2
    {
        const string getLicenseInfoBody = @"licenseInfo.activated = true;
        licenseInfo.flow = licenseCore.licenseKinds.ENTERPRISE;
        licenseInfo.label = licenseCore.licenseKinds.ENTERPRISE;
        licenseInfo.offlineDisabled = false;
        licenseInfo.transactionId = licenseCore.getTransactionId();
        licenseInfo.startDate = new Date('2023-04-16T09:00:00.000Z');
        licenseInfo.stopDate = new Date('2025-04-16T09:00:00.000Z');
        licenseInfo.displayedStopDate = false;
        licenseInfo.canExpire = false;
        const licenseInfoString = JSON.stringify(licenseInfo);
        if (callback !== undefined) {
            callback(undefined, licenseInfoString);
        }
        return Promise.resolve(licenseInfoString);";

        const string getDefaultUserInfoBody = @"return {
            accessToken: '',
            displayName: 'shiroineko',
            organizationForeignKeys: '',
            primaryOrg: '',
            userId: 'shiroineko',
            name: 'shiroineko',
            valid: false,
            whitelisted: true
        };";

        public static bool Patch(string exportFolder)
        {
            var licenseClientPath = Path.Combine(exportFolder, "build/services/licenseService/licenseClient.js");
            var authPath = Path.Combine(exportFolder, "build/services/localAuth/auth.js");

            var licenseClientContent = File.ReadAllText(licenseClientPath);
            var authContent = File.ReadAllText(authPath);

            UnityHubPatcher.ReplaceMethodBody(ref licenseClientContent, "getLicenseInfo", getLicenseInfoBody);
            UnityHubPatcher.ReplaceMethodBody(ref authContent, "getDefaultUserInfo", getDefaultUserInfoBody);

            File.WriteAllText(licenseClientPath, licenseClientContent);
            File.WriteAllText(authPath, authContent);

            return true;
        }
    }
}

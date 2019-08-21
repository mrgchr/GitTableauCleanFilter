# GitTableauCleanFilter
Git clean filter app for tableau workbook to remove thumbnails etc.


# What's this?

Tableau workbook is a big xml file. And you may want to control with Git.

However, Tableau workbook has several issue to handle with SCM.

1. Tableau workbook holds base64 encoded thumbnail image for each sheet, dashboard and story. And you don't want to track the changes in those thumbnails.
1. Tableau workbook holds absolute path to external file like Excel file, csv file, image file, and so on.

This app is a app working with Git clean filter to clean up those SCM-unfriendly elements in tableau workbook file(.tbw).  

# How to use

## install the app
Place `GitTableauCleanFilter.exe` to your Windows PC directory where is on execute PATH is.  
.NET Framework 4.6.1 or above is required.

## edit `.gitattribute` file

Add following line to `.gitattributes` file in your git repository.
```
*.twb filter=tableauworkbook
```

## edit `config`

Execute following command on your console in your git repository.
```
> git config filter.tableauworkbook.clean "GitTableauCleanFilter.exe %f"
```

Or, you can edit `.git\config` file directory. Add following lines to the file.
```
[filter "tableau"]
	clean = GitTableauSmudgeFilter.exe %f
```

If you want to modify global config, you can achieve it by executing following.
```
> git config --global filter.tableauworkbook.clean "GitTableauCleanFilter.exe %f"
```

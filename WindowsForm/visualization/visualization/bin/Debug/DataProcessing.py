import csv
path='D:\Plastics_and_Chemicals_Macro.csv'





def main():
    pass

def ReadFromCSV():
    f = open(path,'r')
    reader=csv.reader(f)
    l=next(reader)
    d=list()
    for row in reader:
        d.append(row)
    f.close()
    return l,d

def HandleData():
    label,data=ReadFromCSV()
    #delete the first null 
    del label[0]
    #re-concat the label list into string 
    label_str=""
    for l in label:
        label_str+=l+","
    label_str=label_str[:len(label_str)-1]
    #re-concat the first column of the data
    #In this case ,the first column is date information
    date_info=""
    date=[column[0] for column in data]
    for d in date:
        date_info+=d+","
    date_info=date_info[:len(date_info)-1]
    #re-concat the data and delete the first column
    data_str=""
    for d in data:
        del d[0]
        for d1 in d:
            data_str+=d1+","
        data_str=data_str[:len(data_str)-1]+'\n'
    
    data_str=data_str[:len(data_str)-1]
    
    print(label_str)
    print(date_info)
    print(data_str)
    #return label_str,date_info,data_str

if __name__=="__main__":
    main()
    HandleData()

    